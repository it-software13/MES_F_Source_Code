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
using static SJeMES_TSM.Process_Selection;

namespace SJeMES_TSM
{
    public partial class Skill_Matrix : MaterialForm
    {
        AutoCompleteStringCollection Autodata;
        public Skill_Matrix()
        {
            InitializeComponent();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Process type");
                return;
            }
            //if (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text))
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Plant and Process type");
            //    return;
            //}
            Cursor.Current = Cursors.WaitCursor;
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("Barcode", txtbcode.Text);
            Data.Add("Process", comboBox1.Text);
            Data.Add("Department", comboBox3.Text);
            Data.Add("ProductionLine", comboBox4.Text);
            Data.Add("Process_Names", txt_processname.Text);
            Data.Add("Production_Plant", comboBox2.Text);
            Data.Add("Month", dt_month.Text);
            Data.Add("Model", txt_model.Text);
            Cursor.Current = Cursors.WaitCursor;
             string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
               Program.Client.APIURL,
               "SJ_TSMAPI",
               "SJ_TSMAPI.Skill_Matrix",
               "GetSkill_Matrix",
               Program.Client.UserToken,
               Newtonsoft.Json.JsonConvert.SerializeObject(Data)
               ); 
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dtJson1;
            }
            else
            {
                dataGridView1.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
            }
        }
        public void LoadProd_Line()
        {
           // textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> kk = new Dictionary<string, string>();
            string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TSMAPI",
                                            "SJ_TSMAPI.Production_Adjustment",
                                            "Get_Prod_line",
            Program.Client.UserToken, JsonConvert.SerializeObject(kk));
            ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
            Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            dt = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            if (dt.Rows.Count > 0)
            { 
                autocompleteMenu1.MaximumSize = new Size(250, 350);
                var columnWidth = new[] { 50, 200 };
                int n = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["department_code"].ToString() }, dt.Rows[i]["department_code"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                    n++;

                }
            }
        }

        public void LoadProd_Plant()
        {
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("Production_plant", comboBox2.Text);
                string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TSMAPI",
                                            "SJ_TSMAPI.Skill_Matrix",
                                            "Get_Prod_Plant",
                    Program.Client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dtJson1.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtJson1.Rows)
                        {
                            comboBox2.Items.Add(dr["department_code"].ToString());
                        }
                        comboBox2.Items.Add("MPAC");
                    }

                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }
        public void Load_Model_Name()
        {
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TSMAPI",
                                            "SJ_TSMAPI.Skill_Matrix",
                                            "Get_Model_Name",
                    Program.Client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        autocompleteMenu2.MaximumSize = new Size(250, 350);
                        var columnWidth = new[] { 50, 200 };
                        int n = 1;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["MODEL_NAME"].ToString() }, dt.Rows[i]["MODEL_NAME"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                            n++;

                        }
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }
        private void Skill_Matrix_Load(object sender, EventArgs e)
        {
            LoadProd_Line();
            LoadProd_Plant();
            Load_Model_Name();
          //  autocompleteMenu1.SetAutocompleteMenu(textBox1, autocompleteMenu1);
            autocompleteMenu2.SetAutocompleteMenu(txt_model, autocompleteMenu2);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No " +
                  
                    "Data Found");
            }
            else
            {
                string a = "Skill_Matrix.xls";
                ExportExcels.Export(a, dataGridView1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Process type");
                return;
            }
            Process_Selection ps = new Process_Selection(comboBox1.Text, txt_model.Text, txt_processname.Text);
            ps.DataChange += new Process_Selection.DataChangeHandler(DataChanged_txtMpo);
            ps.ShowDialog();

        }
        public void DataChanged_txtMpo(object sender, DataChangeEventArgs args)
        {
            txt_processname.Text = args.value1;
            //txtso.ReadOnly = true;
            //GetSTOC_TYPE(Convert.ToString(cbOrg.SelectedValue), textWarehouse1.Text, cbSTOCType);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_processname.Text = "";
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_model_TextChanged(object sender, EventArgs e)
        {
            txt_processname.Text = "";
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetProductionLines();
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetProductionLines();
        }
        public void GetProductionLines()
        {
            DataTable dt = new DataTable();
            Dictionary<string, string> kk = new Dictionary<string, string>();
            kk.Add("Plant", comboBox2.Text);
            kk.Add("Department", comboBox3.Text);
            string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TSMAPI",
                                            "SJ_TSMAPI.Skill_Matrix",
                                            "Get_Prod_line",
            Program.Client.UserToken, JsonConvert.SerializeObject(kk));
            ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
            Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            dt = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            if (dt.Rows.Count > 0)
            {
                comboBox4.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox4.Items.Add(dr["department_code"].ToString());
                }
            }
            else
            {
                comboBox4.Items.Clear();
            }
        }
    }
}
