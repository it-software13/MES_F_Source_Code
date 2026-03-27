using MaterialSkin.Controls;
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

namespace SJeMES_TSM
{
    public partial class Skill_Matrix_Report : MaterialForm
    {
        public Skill_Matrix_Report()
        {
            InitializeComponent();
        }
        public class BGradeReasonData
        {
            public List<Dictionary<string, object>> Data { get; set; }
        }
        public void Plantload()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("Plant", comboBox1.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_TSMAPI",
                       "SJ_TSMAPI.Skill_matrix_Report",
                     "GetPlantData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);

            if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
            {
                comboBox1.Text = "";
                comboBox1.Items.Clear();
                var reasonData = JsonConvert.DeserializeObject<BGradeReasonData>(result.RetData);

                if (reasonData != null && reasonData.Data != null && reasonData.Data.Count > 0)
                {
                    foreach (var row in reasonData.Data)
                    {
                        if (row != null && row.ContainsKey("UDF05"))
                        {
                            var factory = row["UDF05"];
                            if (factory != null)
                            {
                                comboBox1.Items.Add(factory.ToString());
                            }
                        }
                    }
                }
                else
                {
                    comboBox1.Text = " ";
                }
            }
        }
        public void Processload()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("Plant", comboBox1.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_TSMAPI",
                       "SJ_TSMAPI.Skill_matrix_Report",
                     "GetProcessData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);

            if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
            {
                comboBox2.Text = "";
                comboBox2.Items.Clear();
                var reasonData = JsonConvert.DeserializeObject<BGradeReasonData>(result.RetData);

                if (reasonData != null && reasonData.Data != null && reasonData.Data.Count > 0)
                {
                    foreach (var row in reasonData.Data)
                    {
                        if (row != null && row.ContainsKey("UDF01"))
                        {
                            var factory = row["UDF01"];
                            if (factory != null)
                            {
                                comboBox2.Items.Add(factory.ToString());
                            }
                        }
                    }
                }
                else
                {
                    comboBox2.Text = " ";
                }
            }
        }
        public void Lineload()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            

            data.Add("Plant", comboBox1.Text);
            data.Add("Process", comboBox2.Text);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_TSMAPI",
                       "SJ_TSMAPI.Skill_matrix_Report",
                     "GetLineData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);

            if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
            {
                comboBox3.Text = "";
                comboBox3.Items.Clear();
                var reasonData = JsonConvert.DeserializeObject<BGradeReasonData>(result.RetData);

                if (reasonData != null && reasonData.Data != null && reasonData.Data.Count > 0)
                {
                    foreach (var row in reasonData.Data)
                    {
                        if (row != null && row.ContainsKey("DEP_SAP"))
                        {
                            var factory = row["DEP_SAP"];
                            if (factory != null)
                            {
                                comboBox3.Items.Add(factory.ToString());
                            }
                        }
                    }
                }
                else
                {
                    comboBox3.Text = " ";
                }
            }
        }
        public void Skilltypeload()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Line", comboBox3.Text);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_TSMAPI",
                       "SJ_TSMAPI.Skill_matrix_Report",
                     "GetskilltypeData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);

            if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
            {
                comboBox4.Text = "";
                comboBox4.Items.Clear();
                var reasonData = JsonConvert.DeserializeObject<BGradeReasonData>(result.RetData);

                if (reasonData != null && reasonData.Data != null && reasonData.Data.Count > 0)
                {
                    foreach (var row in reasonData.Data)
                    {
                        if (row != null && row.ContainsKey("SKILL_NAME"))
                        {
                            var factory = row["SKILL_NAME"];
                            if (factory != null)
                            {
                                comboBox4.Items.Add(factory.ToString());
                            }
                        }
                    }
                }
                else
                {
                    comboBox4.Text = " ";
                }
            }
        }
        private void Skill_Matrix_Report_Load(object sender, EventArgs e)
        {
            Plantload();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Processload();
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Skilltypeload();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lineload();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {

                Cursor.Current = Cursors.WaitCursor;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Plant", comboBox1.Text);
                data.Add("Process", comboBox2.Text);
                data.Add("Line", comboBox3.Text);
                data.Add("Skill_type", comboBox4.Text);
                string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_TSMAPI",
                       "SJ_TSMAPI.Skill_matrix_Report",
                     "GetskillReportDetails", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);


                var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                if (dic.ContainsKey("data") || dic.ContainsKey("Data"))
                {
                    string dataString = dic.ContainsKey("data") ? dic["data"].ToString() : dic["Data"].ToString();
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dataString);

                    if (dtJson1.Rows.Count > 0)
                        dataGridView1.DataSource = dtJson1;
                    else
                    {
                        dataGridView1.DataSource = null;
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                    }
                }
                else
                {
                    dataGridView1.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Key 'data' not found in response.");
                }

            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");

            }
            else
            {
                string a = "APC_ProdLine_Skill_Matrix_data.xls";
                ExportExcels.Export(a, dataGridView1);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }
    }
}
