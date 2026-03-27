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
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;

namespace SJeMES_TSM
{
    public partial class B_Grade_Repairs_Entry : MaterialForm
    {
        AutoCompleteStringCollection Autodata;
        public B_Grade_Repairs_Entry()
        {
            InitializeComponent();
        }

        private void B_Grade_Repairs_Entry_Load(object sender, EventArgs e)
        {
            LoadProd_Line();
            autocompleteMenu1.SetAutocompleteMenu(textBox1, autocompleteMenu1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clear();
        }
        public void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Production Line");
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Total Received");
                return;
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Total Repaired");
                return;
            } 
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Issues type");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("ProductionLine", textBox1.Text);
            Data.Add("TotalReceived", textBox2.Text);
            Data.Add("TotalRepaired", textBox3.Text);
            Data.Add("TotalUnRepaired", textBox4.Text);
            Data.Add("IssueType", comboBox1.Text); 
            Cursor.Current = Cursors.WaitCursor;
            string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
               Program.Client.APIURL,
               "SJ_TSMAPI",
               "SJ_TSMAPI.B_Grades_Data",
               "Insert_BGrade_Repairs_Data",
               Program.Client.UserToken,
               Newtonsoft.Json.JsonConvert.SerializeObject(Data)
               );
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
            if (ret.IsSuccess)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Added Successfully.");
                clear();
               
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to Add");
                clear();
            }
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text))
            {
                textBox4.Text = (Convert.ToInt32(textBox2.Text) - Convert.ToInt32(textBox3.Text)).ToString();
            }
            else
            {
                textBox4.Text = "";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text))
            {
                if(Convert.ToInt32(textBox2.Text) - Convert.ToInt32(textBox3.Text)>=0)
                {
                    textBox4.Text = (Convert.ToInt32(textBox2.Text) - Convert.ToInt32(textBox3.Text)).ToString();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Repair Quantity should be less than total quantity");
                    textBox3.Text = ""; 
                }
                
            }
            else
            {
                textBox4.Text = "";
            }
        }
    }
}
