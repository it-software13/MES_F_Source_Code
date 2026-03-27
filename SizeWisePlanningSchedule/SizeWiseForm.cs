using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SJeMES_Framework.WebAPI;
using PlanningSchedule;
using Newtonsoft.Json;
using SJeMES_Framework.Common;
using System.Net.Sockets ;
using System.Net;

namespace SizeWisePlanningSchedule
{
    public partial class SizeWiseForm : Form
    {
        private HashSet<string> _allocatedSizes;
        private Dictionary<string, List<string>> _lineSizeMap = new Dictionary<string, List<string>>();
        private List<string> _soList;
        bool savestatus = false; 
        bool stitstatus = false; 
        public SizeWiseForm(HashSet<string> allocatedSizes, Dictionary<string, List<string>> lineSizeMap, List<string> solist , bool status ) 
        {
            InitializeComponent();
            _allocatedSizes = allocatedSizes;
            _lineSizeMap = lineSizeMap;
            _soList = solist; 
            stitstatus = status; 
        }
        private void DesignDataGridView(DataGridView dgv)
        {
            // General grid style
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.LightGray;
            dgv.BorderStyle = BorderStyle.FixedSingle;
            dgv.EnableHeadersVisualStyles = false;
            dgv.AllowUserToOrderColumns = true;
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
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 185, 128);
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

                // Prevent resizing
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        } 


        private void SizeWiseForm_Load(object sender, EventArgs e)
        {
            foreach (var line in _lineSizeMap)
            {
                string lineName = line.Key;
                string sizes = string.Join(", ", line.Value);
                listBox2.Items.Add($"{lineName} → {sizes}");
            }

            GetSizeWiseSOAllocation();
        } 

        private void GetSizeWiseSOAllocation() 
        { 
            try 
            { 
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("SOList", _soList);

                string retdata = WebAPIHelper.Post(
                    PlanningSchedule.Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.SizePlanningController",
                    "GetSizeWiseSOAllocation",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(data)
                );
                string sizeretdata = WebAPIHelper.Post(
                    PlanningSchedule.Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.SizePlanningController",
                    "GetSizeWiseSOData",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(data)
                );

                ResultObject soret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject sizesoret = JsonConvert.DeserializeObject<ResultObject>(sizeretdata);
                if (soret.IsSuccess && soret.RetData != null && soret.RetData.ToString() != "" &&
             sizesoret.IsSuccess && sizesoret.RetData != null && sizesoret.RetData.ToString() != "")
                {
                    DataTable dtSO = JsonHelper.GetDataTableByJson(soret.RetData.ToString());
                    DataTable dtSize = JsonHelper.GetDataTableByJson(sizesoret.RetData.ToString());

                    var sizeColumns = dtSize.AsEnumerable()
                                            .Select(r => r["SIZE_NO"].ToString())
                                            .Distinct()
                                            .OrderBy(s => s)
                                            .ToList();
                     
                    foreach (string size in sizeColumns) 
                    {
                        if (!dtSO.Columns.Contains(size))
                            dtSO.Columns.Add(size, typeof(decimal));
                    }

                    foreach (DataRow soRow in dtSO.Rows)
                    {
                        string seId = soRow["SE_ID"].ToString();

                        var sizeRows = dtSize.AsEnumerable()
                                             .Where(r => r["SE_ID"].ToString() == seId);

                        foreach (var sRow in sizeRows)
                        {
                            string size = sRow["SIZE_NO"].ToString();
                            decimal qty = Convert.ToDecimal(sRow["SE_QTY"]);
                            soRow[size] = qty;
                        }
                    }
                    foreach (var line in _lineSizeMap)
                    {
                        string lineName = line.Key;
                        if (!dtSO.Columns.Contains(lineName))
                            dtSO.Columns.Add(lineName, typeof(decimal));
                    } 
                    foreach (DataRow soRow in dtSO.Rows)
                    {
                        foreach (var line in _lineSizeMap)
                        {
                            string lineName = line.Key;
                            var sizeList = line.Value;
                            decimal totalQty = 0;

                            foreach (string size in sizeList)
                            {
                                if (dtSO.Columns.Contains(size) && soRow[size] != DBNull.Value)
                                    totalQty += Convert.ToDecimal(soRow[size]);
                            }

                            soRow[lineName] = totalQty;
                        }
                    }
                    foreach (var kvp in _lineSizeMap)
                    {
                        string lineName = kvp.Key;
                        List<string> sizes = kvp.Value;
                        var existingSizeIndexes = dtSO.Columns
                                .Cast<DataColumn>()
                                .Select((col, idx) => new { col.ColumnName, Index = idx })
                                .Where(c => sizes.Contains(c.ColumnName))
                                .Select(c => c.Index)
                                .ToList();

                        if (!existingSizeIndexes.Any()) continue;
                        int maxIndex = existingSizeIndexes.Max(); 
                        if (dtSO.Columns.Contains(lineName))
                        {
                            DataColumn col = dtSO.Columns[lineName];
                            col.SetOrdinal(maxIndex + 1);
                        }
                    } 

                    dataGridView1.DataSource = dtSO;
                    DesignDataGridView(dataGridView1); 
                    dataGridView1.ReadOnly = true;  
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    foreach (var line in _lineSizeMap.Keys)
                    {
                        if (dataGridView1.Columns.Contains(line))
                        {
                            dataGridView1.Columns[line].DefaultCellStyle.BackColor = Color.Orange;
                            dataGridView1.Columns[line].DefaultCellStyle.ForeColor = Color.Black;
                            dataGridView1.Columns[line].DefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                        } 
                    }
                }
                else
                {
                    MessageBox.Show("No SO or size data found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while fetching sizes: " + ex.Message);
            }
        }  

        private DataTable GetDataTableFromGrid(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // Create columns
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                dt.Columns.Add(column.HeaderText, column.ValueType ?? typeof(string));
            }

            // Add rows
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        dr[i] = row.Cells[i].Value ?? DBNull.Value;
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
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

        private void button1_Click(object sender, EventArgs e) 
        { 
            try  
            {
                if (!savestatus)
                {
                    DataTable dt = GetDataTableFromGrid(dataGridView1);
                    string jsonData = JsonConvert.SerializeObject(dt);
                    string ipaddress = GetLocalIPAddress();
                    Dictionary<string, object> data = new Dictionary<string, object> { { "schedule", dt } };
                    data.Add("ipaddress", ipaddress);
                    data.Add("linesizemap", _lineSizeMap);
                    data.Add("stitstatus", stitstatus);  

                    string retdata = WebAPIHelper.Post(
                        PlanningSchedule.Program.client.APIURL,
                        "KZ_CUTMNT",
                        "KZ_CUTMNT.Controllers.SizePlanningController",
                        "SaveSizeWiseSchedule",
                        Program.client.UserToken,
                       JsonConvert.SerializeObject(data)
                    );
                    ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata); 
                    if (ret.IsSuccess)
                    {
                        MessageBox.Show("Data inserted successfully!");
                        savestatus = true; 
                    }
                    else
                    {
                        MessageBox.Show("Insert failed: " + ret.ErrMsg);
                    }
                } else
                {
                    MessageBox.Show("The Data Already Saved"); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while sending sizes: " + ex.Message);
            } 
        }

    } 
}
