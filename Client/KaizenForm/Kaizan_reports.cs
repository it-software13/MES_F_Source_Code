using NewExportExcels;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaizenForm
{
    public partial class Kaizan_reports : Form
    {
        public Kaizan_reports()
        {
            InitializeComponent();
        }

        private DataTable ConvertToDataTable(List<Dictionary<string, object>> list)
        {
            DataTable dt = new DataTable();

            if (list == null || list.Count == 0)
                return dt;

            // Add columns to DataTable
            foreach (var key in list[0].Keys)
            {
                dt.Columns.Add(key);
            }

            // Add rows to DataTable
            foreach (var dict in list)
            {
                DataRow row = dt.NewRow();
                foreach (var kvp in dict)
                {
                    row[kvp.Key] = kvp.Value ?? DBNull.Value;
                }
                dt.Rows.Add(row);
            }

            return dt;
        }


        private void BindToGrid(DataTable dtJson)
        {
            if (dtJson.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtJson;
                // Set all cells to ReadOnly
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.ReadOnly = true;
                    }
                }
            }
            else
            {
                dataGridView1.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
            }
        }

       


        private void Button3_Click(object sender, EventArgs e)
        {




        }

        private void Button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "Get_Kaizen_form_details.xls";
                ExportExcels.Export(a, dataGridView1);
                // SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }

        private void Kaizan_reports_Load(object sender, EventArgs e)
        {
           //this.WindowState = FormWindowState.Maximized;
            LoadQueryItem(); 
        }


        public void LoadQueryItem()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Proposer_department", comboBox5.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                                         "KZ_RTDMAPI.Controllers.Kaizenserver",
                                         "GetAllDepts",
                 Program.client.UserToken,
                 Newtonsoft.Json.JsonConvert.SerializeObject(p)
             );
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    comboBox1.Items.Add(dtJson.Rows[i]["DEPARTMENT"].ToString());

                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }



        }

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            string KAIZEN_HEADING = string.Empty;
            string CT_BEFORE = string.Empty;
            string CT_AFTER = string.Empty;
            string MANPOWER_BEFORE = string.Empty;
            string MONTHLY_ORDER_QUANTITY = string.Empty;
            string DEPARTMENT_CODE = string.Empty;
            string MANPOWER_SAVED = string.Empty;
            string MODEL = string.Empty;
            string PROPOSER_DESIGNATION = string.Empty;
            string BEFORE_INSPECTED_QTY = string.Empty;
            string AFTER_INSPECTED_QTY = string.Empty;
            string BEFORE_INSPECTED = string.Empty;
            string AFTER_INSPECTED = string.Empty;
            string RFT_BEFORE = string.Empty;
            string RFT_AFTER = string.Empty;
            string RFT_BEFORE_textBox11 = string.Empty;
            string RFT_AFTER_textBox12 = string.Empty;
            string RFT_SAVINGS = string.Empty;
            string RFT_IMPROVED = string.Empty;
            string SIX_S_BEFORE = string.Empty;
            string SIX_S_AFTER = string.Empty;
            string SIX_S_SAVINGS = string.Empty;
            string SIX_S_IMPROVED = string.Empty;
            string PROPOSER_AREA = string.Empty;
            string PROPOSER_LINE = string.Empty;
            string CT_SAVINGS = string.Empty;
            string BONUS_EVALUATION = string.Empty;
            string MANPOWER_AFTER = string.Empty;
            string MANPOWER_IMPROVED = string.Empty;
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["T_ECRS"].Value.ToString() == "E-Eliminate" || dataGridView1.Rows[e.RowIndex].Cells["T_ECRS"].Value.ToString() == "C-Combine" || dataGridView1.Rows[e.RowIndex].Cells["T_ECRS"].Value.ToString() == "S-Simplify"
                      || dataGridView1.Rows[e.RowIndex].Cells["T_ECRS"].Value.ToString() == "R-Re-arrange")
                {
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("kaizen_number", dataGridView1.Rows[e.RowIndex].Cells["KN"].Value.ToString());
                    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Kaizen_details", Program.client.UserToken, JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                        DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                        for (int i = 0; i < dtJson.Rows.Count; i++)
                        {
                            KAIZEN_HEADING = dtJson.Rows[0]["KAIZEN_HEADING"].ToString();
                            CT_BEFORE = dtJson.Rows[0]["CT_BEFORE"].ToString();
                            CT_AFTER = dtJson.Rows[0]["CT_AFTER"].ToString();
                            MANPOWER_BEFORE = dtJson.Rows[0]["MANPOWER_BEFORE"].ToString();
                            MANPOWER_AFTER = dtJson.Rows[0]["MANPOWER_AFTER"].ToString();
                            MONTHLY_ORDER_QUANTITY = dtJson.Rows[0]["MONTHLY_ORDER_QUANTITY"].ToString();
                            DEPARTMENT_CODE = dtJson.Rows[0]["DEPARTMENT_CODE"].ToString();
                            MANPOWER_SAVED = dtJson.Rows[0]["MANPOWER_SAVED"].ToString();
                            MANPOWER_IMPROVED= dtJson.Rows[0]["MANPOWER_IMPROVED"].ToString();
                            PROPOSER_LINE = dtJson.Rows[0]["PROPOSER_LINE"].ToString();
                            PROPOSER_DESIGNATION = dtJson.Rows[0]["PROPOSER_DESIGNATION"].ToString();
                            CT_SAVINGS = dtJson.Rows[0]["CT_SAVINGS"].ToString();
                            BONUS_EVALUATION = dtJson.Rows[0]["BONUS_EVALUATION"].ToString();
                            MODEL = dtJson.Rows[0]["MODEL"].ToString();
                            PROPOSER_AREA = dtJson.Rows[0]["PROPOSER_AREA"].ToString();
                        }
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                    string KAIZEN_NUMBER = dataGridView1.Rows[e.RowIndex].Cells["KN"].Value.ToString();
                    string KAIZEN_DATE = dataGridView1.Rows[e.RowIndex].Cells["KD"].Value.ToString();
                    string KAIZEN_TYPE = dataGridView1.Rows[e.RowIndex].Cells["KT"].Value.ToString();
                    string PROPOSER_NAME = dataGridView1.Rows[e.RowIndex].Cells["PN"].Value.ToString();
                    string PROPOSER_BARCODE = dataGridView1.Rows[e.RowIndex].Cells["PB"].Value.ToString();
                    //string PROPOSER_AREA = dataGridView1.Rows[e.RowIndex].Cells["PA"].Value.ToString();
                    string PROPOSER_DEPARTMENT = dataGridView1.Rows[e.RowIndex].Cells["PD"].Value.ToString();
                    string CW_NAME = dataGridView1.Rows[e.RowIndex].Cells["CWN"].Value.ToString();
                    string CW_BARCODE = dataGridView1.Rows[e.RowIndex].Cells["CWB"].Value.ToString();
                    string PROJECTED_AREA = dataGridView1.Rows[e.RowIndex].Cells["PA"].Value.ToString();
                    string PROJECTED_DEPARTMENT = dataGridView1.Rows[e.RowIndex].Cells["PRD"].Value.ToString();
                    string PROJECTED_LINE = dataGridView1.Rows[e.RowIndex].Cells["PRL"].Value.ToString();
                    string BEFORE_KAIZEN = dataGridView1.Rows[e.RowIndex].Cells["BK"].Value.ToString();
                    string AFTER_KAIZEN = dataGridView1.Rows[e.RowIndex].Cells["AK"].Value.ToString();
                    string OVERALL_CT_SAVINGS = dataGridView1.Rows[e.RowIndex].Cells["OCT_SAVINGS"].Value.ToString();
                    string KAIZEN_BONUS = dataGridView1.Rows[e.RowIndex].Cells["KB"].Value.ToString();
                    string TYPE_ECRS = dataGridView1.Rows[e.RowIndex].Cells["T_ECRS"].Value.ToString();
                    string STATUS = dataGridView1.Rows[e.RowIndex].Cells["STATUS"].Value.ToString();
                    KaizenForm ru = new KaizenForm(KAIZEN_HEADING, CT_BEFORE, CT_AFTER, MANPOWER_BEFORE, MANPOWER_AFTER, MONTHLY_ORDER_QUANTITY, DEPARTMENT_CODE, MANPOWER_SAVED, MANPOWER_IMPROVED, MODEL, KAIZEN_NUMBER, KAIZEN_DATE, KAIZEN_TYPE, PROPOSER_NAME, PROPOSER_BARCODE, PROPOSER_AREA, PROPOSER_DEPARTMENT, CW_NAME, CW_BARCODE, PROJECTED_AREA,
                    PROJECTED_DEPARTMENT, PROJECTED_LINE, BEFORE_KAIZEN, AFTER_KAIZEN, OVERALL_CT_SAVINGS, KAIZEN_BONUS, TYPE_ECRS, STATUS, PROPOSER_DESIGNATION, CT_SAVINGS, BONUS_EVALUATION, PROPOSER_LINE);
                    ru.Show();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["T_ECRS"].Value.ToString() == "6S")
                {
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("kaizen_number", dataGridView1.Rows[e.RowIndex].Cells["KN"].Value.ToString());
                    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Kaizen_details", Program.client.UserToken, JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                        DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                        for (int i = 0; i < dtJson.Rows.Count; i++)
                        {
                            KAIZEN_HEADING = dtJson.Rows[0]["KAIZEN_HEADING"].ToString();
                            DEPARTMENT_CODE = dtJson.Rows[0]["DEPARTMENT_CODE"].ToString();
                            MODEL = dtJson.Rows[0]["MODEL"].ToString();
                            PROPOSER_DESIGNATION = dtJson.Rows[0]["PROPOSER_DESIGNATION"].ToString();
                            SIX_S_BEFORE = dtJson.Rows[0]["SIX_S_BEFORE"].ToString() + "%";
                            SIX_S_AFTER = dtJson.Rows[0]["SIX_S_AFTER"].ToString() + "%";
                            SIX_S_SAVINGS = dtJson.Rows[0]["SIX_S_SAVINGS"].ToString() + "%";
                            SIX_S_IMPROVED = dtJson.Rows[0]["SIX_S_IMPROVED"].ToString() + "%";
                            PROPOSER_AREA = dtJson.Rows[0]["PROPOSER_AREA"].ToString();
                            PROPOSER_LINE = dtJson.Rows[0]["PROPOSER_LINE"].ToString();
                        }
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                    string KAIZEN_NUMBER = dataGridView1.Rows[e.RowIndex].Cells["KN"].Value.ToString();
                    string KAIZEN_DATE = dataGridView1.Rows[e.RowIndex].Cells["KD"].Value.ToString();
                    string KAIZEN_TYPE = dataGridView1.Rows[e.RowIndex].Cells["KT"].Value.ToString();
                    string PROPOSER_NAME = dataGridView1.Rows[e.RowIndex].Cells["PN"].Value.ToString();
                    string PROPOSER_BARCODE = dataGridView1.Rows[e.RowIndex].Cells["PB"].Value.ToString();
                    //string PROPOSER_AREA = dataGridView1.Rows[e.RowIndex].Cells["PA"].Value.ToString();
                    string PROPOSER_DEPARTMENT = dataGridView1.Rows[e.RowIndex].Cells["PD"].Value.ToString();
                    string CW_NAME = dataGridView1.Rows[e.RowIndex].Cells["CWN"].Value.ToString();
                    string CW_BARCODE = dataGridView1.Rows[e.RowIndex].Cells["CWB"].Value.ToString();
                    string PROJECTED_AREA = dataGridView1.Rows[e.RowIndex].Cells["PA"].Value.ToString();
                    string PROJECTED_DEPARTMENT = dataGridView1.Rows[e.RowIndex].Cells["PRD"].Value.ToString();
                    string PROJECTED_LINE = dataGridView1.Rows[e.RowIndex].Cells["PRL"].Value.ToString();
                    string BEFORE_KAIZEN = dataGridView1.Rows[e.RowIndex].Cells["BK"].Value.ToString();
                    string AFTER_KAIZEN = dataGridView1.Rows[e.RowIndex].Cells["AK"].Value.ToString();
                    //string PROPOSER_AREA = dataGridView1.Rows[e.RowIndex].Cells["PROPOSER_AREA"].Value.ToString();
                    //string PROPOSER_LINE = dataGridView1.Rows[e.RowIndex].Cells["PROPOSER_LINE"].Value.ToString();
                    string TYPE_ECRS = dataGridView1.Rows[e.RowIndex].Cells["T_ECRS"].Value.ToString();
                    string STATUS = dataGridView1.Rows[e.RowIndex].Cells["STATUS"].Value.ToString();
                    _6S__Report ru = new _6S__Report(KAIZEN_HEADING, DEPARTMENT_CODE, MODEL, PROPOSER_DESIGNATION, SIX_S_BEFORE, SIX_S_AFTER, SIX_S_SAVINGS, SIX_S_IMPROVED, KAIZEN_NUMBER, KAIZEN_DATE, KAIZEN_TYPE, PROPOSER_NAME, PROPOSER_BARCODE, PROPOSER_AREA, PROPOSER_DEPARTMENT, CW_NAME, CW_BARCODE, PROJECTED_AREA,
                    PROJECTED_DEPARTMENT, PROJECTED_LINE, BEFORE_KAIZEN, AFTER_KAIZEN,  PROPOSER_LINE, TYPE_ECRS, STATUS);
                    //ru.Show();
                    ru.ShowDialog();

                }
                if (dataGridView1.Rows[e.RowIndex].Cells["T_ECRS"].Value.ToString() == "Quality")
                {
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("kaizen_number", dataGridView1.Rows[e.RowIndex].Cells["KN"].Value.ToString());
                    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Kaizen_details", Program.client.UserToken, JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                        DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                        for (int i = 0; i < dtJson.Rows.Count; i++)
                        {
                            KAIZEN_HEADING = dtJson.Rows[0]["KAIZEN_HEADING"].ToString();
                            DEPARTMENT_CODE = dtJson.Rows[0]["DEPARTMENT_CODE"].ToString();
                            MODEL = dtJson.Rows[0]["MODEL"].ToString();
                            PROPOSER_DESIGNATION = dtJson.Rows[0]["PROPOSER_DESIGNATION"].ToString();
                            BEFORE_INSPECTED_QTY = dtJson.Rows[0]["BEFORE_INSPECTED_QTY"].ToString();
                            AFTER_INSPECTED_QTY = dtJson.Rows[0]["AFTER_INSPECTED_QTY"].ToString();
                            BEFORE_INSPECTED = dtJson.Rows[0]["BEFORE_INSPECTED"].ToString();
                            AFTER_INSPECTED = dtJson.Rows[0]["AFTER_INSPECTED"].ToString();
                            RFT_BEFORE = dtJson.Rows[0]["RFT_BEFORE"].ToString() + "%";
                            RFT_AFTER = dtJson.Rows[0]["RFT_AFTER"].ToString() + "%";
                            RFT_BEFORE_textBox11 = dtJson.Rows[0]["RFT_BEFORE"].ToString() + "%";
                            RFT_AFTER_textBox12 = dtJson.Rows[0]["RFT_AFTER"].ToString() + "%";
                            RFT_SAVINGS = dtJson.Rows[0]["RFT_SAVINGS"].ToString() + "%";
                            RFT_IMPROVED = dtJson.Rows[0]["RFT_IMPROVED"].ToString() + "%";
                            PROPOSER_LINE = dtJson.Rows[0]["PROPOSER_LINE"].ToString();
                        }
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                    string KAIZEN_NUMBER = dataGridView1.Rows[e.RowIndex].Cells["KN"].Value.ToString();
                    string KAIZEN_DATE = dataGridView1.Rows[e.RowIndex].Cells["KD"].Value.ToString();
                    string KAIZEN_TYPE = dataGridView1.Rows[e.RowIndex].Cells["KT"].Value.ToString();
                    string PROPOSER_NAME = dataGridView1.Rows[e.RowIndex].Cells["PN"].Value.ToString();
                    string PROPOSER_BARCODE = dataGridView1.Rows[e.RowIndex].Cells["PB"].Value.ToString();
                    //string PROPOSER_AREA = dataGridView1.Rows[e.RowIndex].Cells["PA"].Value.ToString();
                    string PROPOSER_DEPARTMENT = dataGridView1.Rows[e.RowIndex].Cells["PD"].Value.ToString();
                    string CW_NAME = dataGridView1.Rows[e.RowIndex].Cells["CWN"].Value.ToString();
                    string CW_BARCODE = dataGridView1.Rows[e.RowIndex].Cells["CWB"].Value.ToString();
                    string PROJECTED_AREA = dataGridView1.Rows[e.RowIndex].Cells["PA"].Value.ToString();
                    string PROJECTED_DEPARTMENT = dataGridView1.Rows[e.RowIndex].Cells["PRD"].Value.ToString();
                    string PROJECTED_LINE = dataGridView1.Rows[e.RowIndex].Cells["PRL"].Value.ToString();
                    string BEFORE_KAIZEN = dataGridView1.Rows[e.RowIndex].Cells["BK"].Value.ToString();
                    string AFTER_KAIZEN = dataGridView1.Rows[e.RowIndex].Cells["AK"].Value.ToString();
                    string TYPE_ECRS = dataGridView1.Rows[e.RowIndex].Cells["T_ECRS"].Value.ToString();
                    string STATUS = dataGridView1.Rows[e.RowIndex].Cells["STATUS"].Value.ToString();
                    Quality_Kaizen ru = new Quality_Kaizen(KAIZEN_HEADING, DEPARTMENT_CODE, MODEL, PROPOSER_DESIGNATION, PROPOSER_LINE, BEFORE_INSPECTED_QTY, AFTER_INSPECTED_QTY, BEFORE_INSPECTED, AFTER_INSPECTED, RFT_BEFORE, RFT_AFTER,
                        RFT_BEFORE_textBox11, RFT_AFTER_textBox12, RFT_SAVINGS, RFT_IMPROVED, KAIZEN_NUMBER, KAIZEN_DATE, KAIZEN_TYPE, PROPOSER_NAME, PROPOSER_BARCODE, PROPOSER_AREA, PROPOSER_DEPARTMENT, CW_NAME, CW_BARCODE, PROJECTED_AREA,
                    PROJECTED_DEPARTMENT, PROJECTED_LINE, BEFORE_KAIZEN, AFTER_KAIZEN, TYPE_ECRS, STATUS);
                    ru.Show();
                }
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("fromDate", dateTimePicker1.Text);
            p.Add("toDate", dateTimePicker2.Text);
            p.Add("Kaizen_number", textBox1.Text);
            p.Add("Proposer_Department", comboBox5.Text);
            p.Add("ECRS_TYPE", comboBox2.Text);
            p.Add("Projected_area", comboBox1.Text);
            p.Add("Status", comboBox6.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Get_kaizen_reports", Program.client.UserToken, JsonConvert.SerializeObject(p));
            ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(ret);
            if (ret1.IsSuccess)
            {
                string jsonData = ret1.RetData;
                List<Dictionary<string, object>> listData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                DataTable dtJson = ConvertToDataTable(listData);
                BindToGrid(dtJson);
            }
        }
        




    }
}
