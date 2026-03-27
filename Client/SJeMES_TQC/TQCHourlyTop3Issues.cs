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
using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;

namespace SJeMES_TQC
{
    public partial class TQCHourlyTop3Issues : MaterialForm 
    {
        AutoCompleteStringCollection Autodata;
        public TQCHourlyTop3Issues()
        {
            InitializeComponent(); 
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if (string.IsNullOrEmpty(textBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Production Line");
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                string startdate = dateTimePicker1.Text;
                string enddate = dateTimePicker2.Text;
                string prod_line = textBox1.Text;

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("startdate", startdate);
                p.Add("enddate", enddate);
                p.Add("prod_line", prod_line);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TQCAPI",
                                            "SJ_TQCAPI.TQC_Task",
                                            "Get_Hourly_Top3Issues",
                                            Program.Client.UserToken,
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                //Cursor.Current = Cursors.WaitCursor;
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["prod_date"].Value = dr["createdate"].ToString();
                        dgvr.Cells["production_line_code"].Value = dr["production_line_code"].ToString(); 
                        dgvr.Cells["timeslot"].Value = dr["timeslot"].ToString();
                        dgvr.Cells["total_insp"].Value = dr["total_insp"].ToString();
                        dgvr.Cells["total_pass"].Value = dr["total_pass"].ToString();
                        dgvr.Cells["RFT"].Value = dr["passrate"].ToString();
                        dgvr.Cells["total_defects"].Value = dr["total_defects"].ToString();
                        dgvr.Cells["inspection_name_1"].Value = dr["inspection_name_1"].ToString();
                        dgvr.Cells["total_1"].Value = dr["total_1"].ToString();
                        dgvr.Cells["percentage_1"].Value = dr["percentage_1"].ToString();
                        dgvr.Cells["inspection_name_2"].Value = dr["inspection_name_2"].ToString();
                        dgvr.Cells["total_2"].Value = dr["total_2"].ToString();
                        dgvr.Cells["percentage_2"].Value = dr["percentage_2"].ToString();
                        dgvr.Cells["inspection_name_3"].Value = dr["inspection_name_3"].ToString(); 
                        dgvr.Cells["total_3"].Value = dr["total_3"].ToString();
                        dgvr.Cells["percentage_3"].Value = dr["percentage_3"].ToString();
                        i++;
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                }
            }
            
        }

        private void TQCHourlyTop3Issues_Load(object sender, EventArgs e)
        {
            LoadProd_Line();
            autocompleteMenu1.SetAutocompleteMenu(textBox1, autocompleteMenu1);
            // no smaller than design time size
            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);

            // no larger than screen size
            this.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }
        public void LoadProd_Line()
        {

            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> kk = new Dictionary<string, string>();
            string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TQCAPI",
                                            "SJ_TQCAPI.TQC_Task",
                                            "Get_Prod_line",
           Program.Client.UserToken, JsonConvert.SerializeObject(kk));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString()); 
            if (dt.Rows.Count <= 0)
                {
                   // MessageBox.Show("No data Found!");
                    //return;
                }
                else
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
