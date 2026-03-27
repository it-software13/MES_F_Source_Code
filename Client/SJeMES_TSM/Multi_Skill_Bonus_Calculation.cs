using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutocompleteMenuNS;
using MaterialSkin.Controls;
using NewExportExcels;
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;

namespace SJeMES_TSM
{
    public partial class Multi_Skill_Bonus_Calculation : MaterialForm
    {
        AutoCompleteStringCollection Autodata;
        public Multi_Skill_Bonus_Calculation()
        {
            InitializeComponent();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            Get_Skill_Bonus();
        }

        private void Multi_Skill_Bonus_Calculation_Load(object sender, EventArgs e)
        {
            //LoadProd_Line();
            autocompleteMenu1.SetAutocompleteMenu(txtprodline, autocompleteMenu1);
            autocompleteMenu1.SetAutocompleteMenu(textBox1, autocompleteMenu1);
        }

        private void Get_Skill_Bonus()
        {
            if (string.IsNullOrEmpty(cbprocesstype.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Process type");
                return;
            }
            if (string.IsNullOrEmpty(txtprodline.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Production Plant");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("ProdLine", txtprodline.Text);
            p.Add("ProcessType", cbprocesstype.Text);
            p.Add("Barcode", txtbcode.Text);
            p.Add("Month", dateTimePicker1.Text); 
            string responseData = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TSMAPI", "SJ_TSMAPI.Multi_Skill_Bonus_Calculation", "Get_Skill_Bonus", Program.Client.UserToken, JsonConvert.SerializeObject(p)); 
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData); 
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dtJson1.Rows.Count > 0)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dtJson1;
                }
                else
                {
                    dataGridView1.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Skill or Attandance Data is not updated.");
                }
            }
        }

        public void LoadProd_Line()
        {
            txtprodline.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtprodline.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
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

        private void btnexport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string month = dateTimePicker1.Text;
                string month2 = month.Replace("/", "");
                string a = "Skill_Bonus_"+month2+".xls";
                ExportExcels.Export(a, dataGridView1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Get_Skill_Bonus_Eligible_List();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string month = dateTimePicker1.Text;
                string month2 = month.Replace("/", "");
                string a = "Skill_Bonus_Eligible_List" + month2 + ".xls";
                ExportExcels.Export(a, dataGridView2);
            }
        }

        private void Get_Skill_Bonus_Eligible_List()
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Process type");
                return;
            }
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Production Plant");
                return;
            }
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("ProdLine", textBox1.Text);
            p.Add("ProcessType", comboBox1.Text);
            p.Add("Barcode", textBox2.Text);
            p.Add("Month", dateTimePicker2.Text);
            string responseData = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TSMAPI", "SJ_TSMAPI.Multi_Skill_Bonus_Calculation", "Get_Skill_Bonus_Eligible_List", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dtJson1.Rows.Count > 0)
                {
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = dtJson1;
                }
                else
                {
                    dataGridView2.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Employee eligible for Bonus");
                }

            }
        }

        private void TableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
