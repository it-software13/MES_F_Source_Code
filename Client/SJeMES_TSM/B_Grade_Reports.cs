using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using AutocompleteMenuNS;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;

namespace SJeMES_TSM
{
    public partial class B_Grade_Reports : MaterialForm 
    {
        AutoCompleteStringCollection Autodata;
        public B_Grade_Reports()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            B_Grade_Repairs_Entry BR = new B_Grade_Repairs_Entry();
            BR.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string fromDate = dateTimePicker1.Text;
                string toDate = dateTimePicker2.Text;
                string Issue_Type = comboBox1.Text;
                string Production_Line = textBox1.Text; 
                dataGridView1.DataSource = null;
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("fromDate", fromDate);
                p.Add("toDate", toDate);
                p.Add("Issue_Type", Issue_Type);
                p.Add("Production_Line", Production_Line); 
                string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                       Program.Client.APIURL,
                       "SJ_TSMAPI",
                       "SJ_TSMAPI.B_Grades_Data",
                       "Get_BGrade_Repairs_Data",
                       Program.Client.UserToken,
                       Newtonsoft.Json.JsonConvert.SerializeObject(p)
                   );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);


                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                    if (dtJson1.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dtJson1;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                    }
                }
                else
                {
                    dataGridView1.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }

            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void B_Grade_Reports_Load(object sender, EventArgs e)
        {
            LoadProd_Line();
            autocompleteMenu1.SetAutocompleteMenu(textBox1, autocompleteMenu1);
        }
        public void LoadProd_Line()
        {
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
    }
}
