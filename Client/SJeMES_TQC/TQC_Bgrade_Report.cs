using AutocompleteMenuNS;
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

namespace SJeMES_TQC
{
    public partial class TQC_Bgrade_Report : MaterialForm
    {
        public class BGradeReasonData
        {
            public List<Dictionary<string, object>> Data { get; set; }
        }

        public TQC_Bgrade_Report()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {

                Cursor.Current = Cursors.WaitCursor;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("FromDate", dateTimePicker1.Text);
                data.Add("ToDate", dateTimePicker2.Text);
                data.Add("Org", Org.Text);
                data.Add("Plant", Plant.Text);
                data.Add("Line", Line.Text);
                data.Add("Po", Po.Text);
                data.Add("Bgrade_Reason", Bgrade_Reason.Text);
                string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "TQC_BgradeCount", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

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
        public void Bgrade_Reasons()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("BGRADE_REASON", Bgrade_Reason.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "Bgrade_Reasons", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);

            if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
            {
                Bgrade_Reason.Text = "";
                Bgrade_Reason.Items.Clear();
                var reasonData = JsonConvert.DeserializeObject<BGradeReasonData>(result.RetData);

                if (reasonData != null && reasonData.Data != null && reasonData.Data.Count > 0)
                {
                    foreach (var row in reasonData.Data)
                    {
                        if (row.ContainsKey("BGRADE_REASON"))
                        {
                            Bgrade_Reason.Items.Add(row["BGRADE_REASON"].ToString());
                        }
                    }
                }
                else
                {
                    Bgrade_Reason.Text = "No Data Found";
                }
            }




        }
        public void PlantLoad()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            
            data.Add("Org", Org.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "PlantViewLoad", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);

            if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
            {
                Plant.Text = "";
                Plant.Items.Clear();
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
                                Plant.Items.Add(factory.ToString());
                            }
                        }
                    }
                }
                else
                {
                    Plant.Text = " ";
                }
            }




        }
        public void OrgLoad()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("Org", Org.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "Orgload", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);

            if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
            {
                Org.Text = "";
                Org.Items.Clear();
                var reasonData = JsonConvert.DeserializeObject<BGradeReasonData>(result.RetData);

                if (reasonData != null && reasonData.Data != null && reasonData.Data.Count > 0)
                {
                    foreach (var row in reasonData.Data)
                    {
                        if (row != null && row.ContainsKey("FACTORY_SAP"))
                        {
                            var factory = row["FACTORY_SAP"];
                            if (factory != null)
                            {
                                Org.Items.Add(factory.ToString());
                            }
                        }
                    }

                }
                else
                {
                    Org.Text = " ";
                }
            }




        }
        public void ProdlineLoad()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("Plant", Plant.Text);
            data.Add("Org", Org.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "ProdlineViewload", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);

            if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
            {
                Line.Text = "";
                Line.Items.Clear();
                var reasonData = JsonConvert.DeserializeObject<BGradeReasonData>(result.RetData);

                if (reasonData != null && reasonData.Data != null && reasonData.Data.Count > 0)
                {
                    foreach (var row in reasonData.Data)
                    {
                        if (row != null && row.ContainsKey("DEPARTMENT_CODE"))
                        {
                            var factory = row["DEPARTMENT_CODE"];
                            if (factory != null)
                            {
                                Line.Items.Add(factory.ToString());
                            }
                        }
                    }
                }
                else
                {
                    Line.Text = " ";
                }
            }




        }

        public void Load_Po()
        {

            Po.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Po.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, object> data = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "GetPoData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));


            ResultObject retObject = JsonConvert.DeserializeObject<ResultObject>(ret);
            if (retObject.IsSuccess)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(retObject.RetData);
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dtJson.Rows.Count > 0)
                {
                    autocompleteMenu1.MaximumSize = new Size(250, 350);
                    var columnWidth = new[] { 50, 200 };

                    int n = 1;
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dtJson.Rows[i]["CUSTOMER_PO"].ToString() }, dtJson.Rows[i]["CUSTOMER_PO"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                        n++;
                    }

                }

            }
        }
        private void TQC_Bgrade_View_Load(object sender, EventArgs e)
        {
            Bgrade_Reasons();
            OrgLoad();
            Load_Po();
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProdlineLoad();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlantLoad();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "TQC_Bgrade_Report_Data.xls";
                ExportExcels.Export(a, dataGridView1);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }
    }
}
