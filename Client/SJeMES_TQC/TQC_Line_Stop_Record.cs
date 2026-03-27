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
using SJeMES_Framework.WebAPI;

namespace SJeMES_TQC
{
    public partial class TQC_Line_Stop_Record : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        AutoCompleteStringCollection Autodata;
        public TQC_Line_Stop_Record()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void TQC_Line_Stop_Record_Load(object sender, EventArgs e)
        {
            //LoadProd_Line();
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

        private void button1_Click(object sender, EventArgs e)
        {
                Get_TQC_Stopline_Record(); 
        }

        public void Get_TQC_Stopline_Record()
        {
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
                                        "Get_TQC_Stopline_Record",
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
                    dgvr.Cells["line"].Value = dr["production_line_code"].ToString();
                    dgvr.Cells["stoptime"].Value = dr["Line_Stop_Time"].ToString();
                    dgvr.Cells["starttime"].Value = dr["Line_Start_Time"].ToString();
                    dgvr.Cells["time"].Value = dr["Stop_Time"].ToString();
                    dgvr.Cells["reason"].Value = dr["Stop_Reason"].ToString();
                    i++;
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
        }
    }
}

