using NewExportExcels;
using Newtonsoft.Json;
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
    public partial class Kaizen_KPI : Form
    {
        public Kaizen_KPI()
        {
            InitializeComponent();
        }

        private void Kaizen_KPI_Load(object sender, EventArgs e)
        {
            LoadQueryItem();
        }

        public void LoadQueryItem()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();

            p.Add("Proposer_department", comboBox1.Text);

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

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            //List<string> strList = new List<string>();
            string department = comboBox3.Text;

            if (!string.IsNullOrEmpty(department))
            {
                department = department.Substring(0, 1);
            }

            p.Add("department", department);
            string text = comboBox3.Text;

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

                //comboBox2.Items.Clear();

                if (dtJson.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtJson.Rows)
                    {
                        //comboBox2.Items.Add(dr["DEPARTMENT_CODE"].ToString());
                    }
                }



            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("fromDate", dateTimePicker1.Text);
            p.Add("toDate", dateTimePicker2.Text);
            p.Add("Proposer_Department", comboBox1.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Get_Kaizen_Kpi", Program.client.UserToken, JsonConvert.SerializeObject(p));
            ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(ret);
            if (ret1.IsSuccess)
            {
                string jsonData = ret1.RetData;
                List<Dictionary<string, object>> listData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                DataTable dtJson = ConvertToDataTable(listData);
                BindToGrid(dtJson);
            }

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

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
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
    }
}
