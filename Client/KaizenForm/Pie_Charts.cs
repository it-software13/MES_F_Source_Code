using SJeMES_Framework.WebAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;
using Font = System.Drawing.Font;

namespace KaizenForm
{
    public partial class Pie_Charts : Form
    {
        public Pie_Charts()
        {
            InitializeComponent();
        }

        private void Pie_Charts_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            LoadQueryItem1();
            LoadQueryItem();
             
        }


       

        public void LoadQueryItem1()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Factory_code", comboBox3.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_POSAPI", "KZ_POSAPI.Controllers.POServer", "Get_org_department", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                comboBox3.Items.Clear();
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    comboBox3.Items.Add(dtJson.Rows[i]["FACTORY_SAP"].ToString());

                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
       

        public void LoadQueryItem()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();

            p.Add("Proposer_department", comboBox2.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "GetAllDepts", Program.client.UserToken, JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    comboBox2.Items.Add(dtJson.Rows[i]["DEPARTMENT"].ToString());
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        public void Dashboard()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("fromDate", dateTimePicker1.Text);
            p.Add("toDate", dateTimePicker2.Text);
            p.Add("Proposer_Department", comboBox2.Text);
            string ret = WebAPIHelper.Post(
                Program.client.APIURL,
                "KZ_RTDMAPI",
                "KZ_RTDMAPI.Controllers.Kaizenserver",
                "Get_Kaizen_Kpi",
                Program.client.UserToken,
                JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    if (dtJson.Rows.Count > 0)
                    {
                        chart2.Series.Clear();
                        chart2.ChartAreas.Clear();
                        chart2.Legends.Clear();

                        // Chart area setup
                        ChartArea ca = new ChartArea("ChartArea1");
                        ca.AxisX.Interval = 1;
                        ca.AxisX.MajorGrid.Enabled = false;
                        ca.AxisY.MajorGrid.Enabled = false;
                        ca.AxisX.Title = "Line / Department";
                        ca.AxisY.Title = "Count";
                        Font axisFont = new Font("Lucida Bright", 12, FontStyle.Regular);
                        Font titleFont = new Font("Lucida Bright", 12, FontStyle.Bold);
                        ca.AxisX.LabelStyle.Font = axisFont;
                        ca.AxisY.LabelStyle.Font = axisFont;
                        ca.AxisX.TitleFont = titleFont;
                        ca.AxisY.TitleFont = titleFont;
                        ca.AxisX.LineColor = Color.FromArgb(64, 64, 64);
                        ca.AxisY.LineColor = Color.FromArgb(64, 64, 64);
                        ca.AxisX.LabelStyle.ForeColor = Color.FromArgb(0, 102, 102);
                        ca.AxisY.LabelStyle.ForeColor = Color.FromArgb(0, 102, 204);
                        ca.AxisX.TitleForeColor = Color.Black;
                        ca.AxisY.TitleForeColor = Color.Black;

                        // Scroll settings
                        ca.AxisX.ScrollBar.Enabled = true;
                        ca.AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
                        ca.AxisX.ScrollBar.Size = 12;
                        ca.AxisX.ScaleView.Zoomable = true;
                        ca.AxisX.ScaleView.Size = 6;
                        ca.AxisX.ScaleView.MinSize = 1;
                        chart2.ChartAreas.Add(ca);

                        // Target series   
                        Series targetSeries = new Series("Target")
                        {
                            ChartType = SeriesChartType.Column,
                            Color = Color.FromArgb(0, 153, 153),
                            IsValueShownAsLabel = true,
                            Font = axisFont,
                            MarkerStyle = MarkerStyle.None
                        };
                        targetSeries["PointWidth"] = "0.8";
                        targetSeries.BorderWidth = 0;

                        // Actual series
                        Series actualSeries = new Series("Actual")
                        {
                            ChartType = SeriesChartType.Column,
                            Color = Color.FromArgb(102, 178, 255),
                            IsValueShownAsLabel = true,
                            Font = axisFont,
                            MarkerStyle = MarkerStyle.None
                        };
                        actualSeries["PointWidth"] = "0.8";
                        actualSeries.BorderWidth = 0;

                        // Fill series safely
                        foreach (DataRow row in dtJson.Rows)
                        {
                            string label = string.Empty;

                            // ✅ Handle Department vs Line dynamically
                            if (dtJson.Columns.Contains("PLANT"))
                            {
                                label = row["PLANT"].ToString(); // Department view
                            }
                            else if (dtJson.Columns.Contains("LINE"))
                            {
                                label = row["LINE"].ToString();  // 🔹 Only line name (no department prefix)
                            }

                            double targetValue = 0;
                            double actualValue = 0;

                            if (row["TARGETS"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["TARGETS"].ToString()))
                                double.TryParse(row["TARGETS"].ToString(), out targetValue);

                            if (row["ACTUAL_KAIZEN_COUNT"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["ACTUAL_KAIZEN_COUNT"].ToString()))
                                double.TryParse(row["ACTUAL_KAIZEN_COUNT"].ToString(), out actualValue);

                            targetSeries.Points.AddXY(label, targetValue);
                            actualSeries.Points.AddXY(label, actualValue);
                        }
                        chart2.Series.Add(targetSeries);
                        chart2.Series.Add(actualSeries);

                        // ✅ Only show numeric labels above bars
                        foreach (var point in targetSeries.Points)
                        {
                            point.Label = point.YValues[0].ToString();
                            point.LabelForeColor = Color.Black;
                            point.Font = new Font("Lucida Bright", 10, FontStyle.Bold);
                        }

                        foreach (var point in actualSeries.Points)
                        {
                            point.Label = point.YValues[0].ToString();
                            point.LabelForeColor = Color.Black;
                            point.Font = new Font("Lucida Bright", 10, FontStyle.Bold);
                        }

                        // Legend
                        Legend legend = new Legend("Legend");
                        legend.Docking = Docking.Top;
                        legend.Font = axisFont;
                        legend.BackColor = Color.White;
                        chart2.Legends.Add(legend);
                        targetSeries.Legend = "Legend";
                        actualSeries.Legend = "Legend";

                        // Titles
                        chart2.Titles.Clear();
                        chart2.Titles.Add(new Title("Target vs Actual Kaizens",
                            Docking.Top, titleFont, Color.Red));
                        chart2.Titles.Add(new Title("Kaizen KPI Achievement",
                            Docking.Top, axisFont, Color.Black));

                        if (dtJson.Rows.Count > 6)
                        {
                            ca.AxisX.ScaleView.Position = 0;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data found for the selected criteria.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (JsonSerializationException ex)
                {
                    MessageBox.Show("Error parsing data: " + ex.Message);
                }


            }
            else
            {
                string errorMsg = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, errorMsg);
            }
        }


        private void LoadSamplePieChart()
        {
            try
            {
                // ✅ Use selected range (fromDate to toDate)
                DateTime fromDate = dateTimePicker1.Value; // FromDate
                DateTime toDate = dateTimePicker2.Value;   // ToDate

                Dictionary<string, object> p = new Dictionary<string, object>
        {
            { "fromDate", fromDate.ToString("yyyy/MM/dd") },
            { "toDate", toDate.ToString("yyyy/MM/dd") }
        };

                // Call API
                string ret = WebAPIHelper.Post(
                    Program.client.APIURL,
                    "KZ_RTDMAPI",
                    "KZ_RTDMAPI.Controllers.Kaizenserver",
                    "Get_Month_Wise_Kaizen",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));

                var apiResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                bool isSuccess = Convert.ToBoolean(apiResponse["IsSuccess"]);
                if (!isSuccess)
                {
                    MessageBox.Show("API call failed: " + apiResponse["ErrMsg"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Parse result
                string json = apiResponse["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                // Build dictionary from API (only months returned)
                Dictionary<string, int> monthData = new Dictionary<string, int>();
                foreach (DataRow row in dt.Rows)
                {
                    string monthYear = row["MONTH_YEAR"].ToString(); // e.g. "Jul 2025"
                    int count = Convert.ToInt32(row["RAISED_COUNT"]);
                    if (!monthData.ContainsKey(monthYear))
                        monthData.Add(monthYear, count);
                }

                // ✅ Build all months between fromDate and toDate
                List<string> monthsInRange = new List<string>();
                DateTime iterDate = new DateTime(fromDate.Year, fromDate.Month, 1);
                while (iterDate <= toDate)
                {
                    monthsInRange.Add(iterDate.ToString("MMM yyyy"));
                    iterDate = iterDate.AddMonths(1);
                }

                // Clear chart
                chart3.Series.Clear();
                chart3.ChartAreas.Clear();

                Series series = new Series("Kaizen Trend")
                {
                    ChartType = SeriesChartType.Spline,
                    BorderWidth = 3,
                    Color = Color.Teal,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 6
                };

                // ✅ Add all months (0 if missing)
                foreach (string month in monthsInRange)
                {
                    int count = monthData.ContainsKey(month) ? monthData[month] : 0;
                    DataPoint point = new DataPoint();
                    point.SetValueXY(month, count);
                    // Show label only if > 0
                    if (count > 0)
                    {
                        point.IsValueShownAsLabel = true;
                        point.LabelForeColor = Color.Black;
                        // 👉 changed to normal font
                        point.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    }

                    series.Points.Add(point);
                }

                chart3.Series.Add(series);

                // Configure chart area
                ChartArea chartArea = new ChartArea("MainArea");
                chartArea.BackColor = Color.White;

                chartArea.AxisX.Interval = 1;
                // 👉 changed to normal font
                chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                chartArea.AxisX.MajorGrid.Enabled = false;
                chartArea.AxisX.LineColor = Color.LightGray;

                // 👉 changed to normal font
                chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                chartArea.AxisY.MajorGrid.Enabled = false; // Removes horizontal grid lines
                chartArea.AxisY.LineColor = Color.LightGray;

                chart3.ChartAreas.Add(chartArea);

                if (chart3.Legends.Count > 0)
                    chart3.Legends[0].Enabled = false;

                chart3.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading chart: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("fromDate", dateTimePicker1.Text);
            p.Add("toDate", dateTimePicker2.Text);
            p.Add("Proposer_Dept", comboBox2.Text);
            string ret = WebAPIHelper.Post(
               Program.client.APIURL,
               "KZ_RTDMAPI",
               "KZ_RTDMAPI.Controllers.Kaizenserver",
               "Get_Dashboard_reports",
               Program.client.UserToken,
               JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert
                    .DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();

                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    // ✅ Handle null / empty / zero-data case
                    if (dtJson == null || dtJson.Rows.Count == 0 ||
                        dtJson.AsEnumerable().All(r => r["COUNT"] == DBNull.Value || Convert.ToDouble(r["COUNT"]) == 0))
                    {
                        chart1.Series.Clear();
                        chart1.ChartAreas.Clear();
                        chart1.Legends.Clear();
                        chart1.Titles.Clear();

                        chart1.Titles.Add(new Title("No data found",
                            Docking.Top,
                            new Font("Tahoma", 14, FontStyle.Bold),
                            Color.Red));

                        return;
                    }

                    // Clear old chart
                    chart1.Series.Clear();
                    chart1.ChartAreas.Clear();
                    chart1.Legends.Clear();

                    // Chart area
                    ChartArea chartArea = new ChartArea("ChartArea1");
                    chartArea.BackColor = Color.Transparent;
                    chartArea.AxisX.MajorGrid.LineWidth = 0;
                    chartArea.AxisY.MajorGrid.LineWidth = 0;
                    chartArea.AxisX.LabelStyle.Enabled = false;
                    chartArea.AxisY.LabelStyle.Enabled = false;
                    chart1.ChartAreas.Add(chartArea);

                    // Pie series
                    Series series = new Series("Arc");
                    series.ChartType = SeriesChartType.Pie;
                    series.IsValueShownAsLabel = false;   // ❌ no inside labels
                    series["PieLabelStyle"] = "Disabled"; // ❌ remove arrows
                    chart1.Series.Add(series);

                    // Legend
                    Legend legend = new Legend("KaizenStatus");
                    legend.Docking = Docking.Right;
                    legend.Alignment = StringAlignment.Center;
                    legend.Title = "Kaizens Raised";
                    legend.TitleFont = new Font("Tahoma", 14, FontStyle.Bold);
                    legend.Font = new Font("Tahoma", 12, FontStyle.Regular);
                    legend.BackColor = Color.Transparent;
                    chart1.Legends.Add(legend);

                    // Total count
                    double total = dtJson.AsEnumerable()
                                         .Where(r => r["COUNT"] != DBNull.Value)
                                         .Sum(r => Convert.ToDouble(r["COUNT"]));

                    foreach (DataRow row in dtJson.Rows)
                    {
                        if (row["COUNT"] == DBNull.Value) continue;

                        string status = row["STATUS"].ToString();
                        double count = Convert.ToDouble(row["COUNT"]);
                        if (count == 0) continue;

                        double percent = (count / total) * 100;

                        // Rename DONE → Approved
                        string rawStatus = status.ToUpper() == "DONE" ? "Approved" : status;

                        // Proper case (First letter capital)
                        string displayStatus = System.Globalization.CultureInfo.CurrentCulture
                            .TextInfo.ToTitleCase(rawStatus.ToLower());

                        // Add point
                        int pointIndex = chart1.Series["Arc"].Points.AddXY(displayStatus, count);

                        // Legend text only (status + % + count)
                        chart1.Series["Arc"].Points[pointIndex].LegendText =
                            $"{displayStatus} ({percent:F0}%, {count})";

                        // Assign fixed colors
                        switch (status.ToUpper())
                        {
                            case "DONE":
                                chart1.Series["Arc"].Points[pointIndex].Color = Color.Teal; // Approved
                                break;
                            case "ON GOING":
                                chart1.Series["Arc"].Points[pointIndex].Color = Color.Orange;
                                break;
                            case "REJECTED":
                                chart1.Series["Arc"].Points[pointIndex].Color = Color.Red;
                                break;
                            case "APPROVED":
                                chart1.Series["Arc"].Points[pointIndex].Color = Color.MediumSeaGreen;
                                break;
                            case "RAISED":
                                chart1.Series["Arc"].Points[pointIndex].Color = Color.DarkCyan;
                                break;
                            default:
                                chart1.Series["Arc"].Points[pointIndex].Color = Color.Gray;
                                break;
                        }
                    }

                    // Optional extra calls (if needed for your app flow)
                    Dashboard();
                    LoadSamplePieChart();
                    FillPiChartIE();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }





        // chart detailed
        private void FillPiChartIE()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("fromDate", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
            p.Add("toDate", dateTimePicker2.Value.ToString("yyyy/MM/dd"));

            string ret = WebAPIHelper.Post(
               Program.client.APIURL,
               "KZ_RTDMAPI",
               "KZ_RTDMAPI.Controllers.Kaizenserver",
               "Get_Ecrs_Wise_Kaizen",
               Program.client.UserToken,
               JsonConvert.SerializeObject(p));

            var resultDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(resultDict["IsSuccess"]))
            {
                string json = resultDict["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        // ✅ Rename column headers (remove underscores)
                        foreach (DataColumn col in dtJson.Columns)
                        {
                            col.ColumnName = col.ColumnName.Replace("_", " ");
                        }

                        // ✅ Bind API data to DataGridView
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = dtJson;

                        StyleLikeCardTable(dataGridView1);
                    }
                    else
                    {
                        // Clear grid if no data
                        dataGridView1.DataSource = null;
                    }
                }
                catch (Newtonsoft.Json.JsonSerializationException ex)
                {
                    Console.WriteLine("Error deserializing JSON: " + ex.Message);
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, resultDict["ErrMsg"].ToString());
            }
        }


        private void StyleLikeCardTable(DataGridView dgv)
        {
            // Define fonts
            Font customFont = new Font("Lucida Bright", 10, FontStyle.Regular);
            Font headerFont = new Font("Microsoft YaHei", 14, FontStyle.Bold);

            // show ONLY horizontal lines
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(230, 230, 230);

            // no left row-header (triangle column)
            dgv.RowHeadersVisible = false;

            // clean header look
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Font = headerFont;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // ✅ Header centered

            // row appearance - apply custom font to all cells
            dgv.BorderStyle = BorderStyle.None;
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = customFont;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 248, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.RowTemplate.Height = 40;
            dgv.DefaultCellStyle.Padding = new Padding(8, 8, 8, 8);

            // ✅ Center align all row text (Lucida Bright part)
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // tidy behavior
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Adjust row height to fit the larger font
            dgv.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }

        private void Export_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG Image|*.png|PDF File|*.pdf";
                sfd.Title = "Export Chart";
                sfd.FileName = "KaizenChart";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (sfd.FilterIndex == 1)
                    {
                        //chart1.SaveImage(sfd.FileName, ChartImageFormat.Png);
                        MessageBox.Show("Chart saved as PNG.");
                    }
                    else if (sfd.FilterIndex == 2)
                    {
                        string imagePath = Path.ChangeExtension(sfd.FileName, ".png");
                        //chart1.SaveImage(imagePath, ChartImageFormat.Png);

                        using (Document doc = new Document())
                        {
                            PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                            doc.Open();
                            iTextSharp.text.Image chartImg = iTextSharp.text.Image.GetInstance(imagePath);
                            chartImg.ScaleToFit(500f, 400f);
                            doc.Add(chartImg);
                            doc.Close();
                        }

                        File.Delete(imagePath);
                        MessageBox.Show("Chart saved as PDF.");
                    }
                }
            }
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            //List<string> strList = new List<string>();
            string department = comboBox4.Text;

            if (!string.IsNullOrEmpty(department))
            {
                department = department.Substring(0, 1);
            }

            p.Add("department", department);
            string text = comboBox4.Text;

            if (!string.IsNullOrEmpty(text))
            {

                text = text.Replace("-", "").ToUpper();
            }

            p.Add("Plant", text);


            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "GetAllcode2", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                comboBox1.Items.Clear();

                if (dtJson.Rows.Count > 0)

                {
                    foreach (DataRow dr in dtJson.Rows)
                    {
                        comboBox1.Items.Add(dr["DEPARTMENT_CODE"].ToString());

                    }
                }



            }

        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
