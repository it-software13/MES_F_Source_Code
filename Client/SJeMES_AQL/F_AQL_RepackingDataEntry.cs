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

namespace SJeMES_AQL
{
    public partial class F_AQL_RepackingDataEntry : MaterialForm
    {
        public F_AQL_RepackingDataEntry()
        {
            InitializeComponent();
        }

        private void F_AQL_RepackingDataEntry_Load(object sender, EventArgs e)
        {
            Get_PO_List();
            Get_Production_Line_List();
            autocompleteMenu1.SetAutocompleteMenu(textBox6, autocompleteMenu1);
            autocompleteMenu1.SetAutocompleteMenu(textBox4, autocompleteMenu1);
            autocompleteMenu2.SetAutocompleteMenu(textBox8, autocompleteMenu2);
            autocompleteMenu2.SetAutocompleteMenu(textBox1, autocompleteMenu2);
            btn_cancel.Visible = false;
            btn_update.Visible = false;
            lbl_update.Visible = false;
            txt_update.Visible = false;
            GetMin_and_MaxDate();
           
        }
        public void GetMin_and_MaxDate()
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",
                                            "SJ_AQLAPI.AQL_Repack_Data",
                                            "GetMin_and_MaxDate",
                                            Program.Client.UserToken,
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    dateTimePicker2.MinDate = Convert.ToDateTime(dt.Rows[0]["MINDATE"]);
                    dateTimePicker2.MaxDate = Convert.ToDateTime(dt.Rows[0]["MAXDATE"]);

                }
                else
                {
                    dateTimePicker2.MinDate = DateTime.Today.AddDays(-3);
                    dateTimePicker2.MaxDate = DateTime.Today;
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        public void Get_Production_Line_List()
        {
            try
            {

                textBox8.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox8.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                Dictionary<string, object> p = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TSMAPI",
                                            "SJ_TSMAPI.Production_Adjustment",
                                            "Get_Prod_line",
                                            Program.Client.UserToken,
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    autocompleteMenu2.MaximumSize = new Size(250, 350);
                    var columnWidth = new[] { 50, 200 };
                    int n = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["department_code"].ToString() }, dt.Rows[i]["department_code"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                        n++;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        public void Get_PO_List()
        {
            try
            {

                textBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox6.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox6.AutoCompleteSource = AutoCompleteSource.CustomSource;
                Dictionary<string, object> p = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",
                                            "SJ_AQLAPI.AQL_Repack_Data",
                                            "Get_PO_List",
                                            Program.Client.UserToken,
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    autocompleteMenu1.MaximumSize = new Size(250, 350);
                    var columnWidth = new[] { 50, 200 };
                    int n = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["MER_PO"].ToString() }, dt.Rows[i]["MER_PO"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                        n++;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        public void Submit_Repack_Data()
        {
            try
            {
               
                if (string.IsNullOrEmpty(textBox8.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Production line");
                    return;
                }

                if (string.IsNullOrEmpty(textBox6.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter PO");
                    return;
                }

                if (string.IsNullOrEmpty(textBox7.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Repack Quantity");
                    return;
                }

                if (string.IsNullOrEmpty(textBox5.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Repack reason");
                    return;
                }
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("po", textBox6.Text);
                p.Add("prod_line", textBox8.Text);
                p.Add("repack_qty", textBox7.Text);
                p.Add("reason", textBox5.Text);
                p.Add("repackdate", dateTimePicker2.Value.ToString("yyyy/MM/dd"));
                p.Add("update_reason", txt_update.Text);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",
                                            "SJ_AQLAPI.AQL_Repack_Data",
                                            "Submit_repack_data",
                                            Program.Client.UserToken,
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Submitted Successfully");
                    clear();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    clear();
                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                clear();
            }
        }

        public void clear()
        {
            textBox6.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
            textBox5.Text = "";
            txt_update.Text = "";
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            Get_Repack_Data();
        }

        public void Get_Repack_Data()
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("po", textBox4.Text);
                p.Add("prod_line", textBox1.Text);
                p.Add("s_date", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
                p.Add("e_date", dateTimePicker3.Value.ToString("yyyy/MM/dd"));
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",
                                            "SJ_AQLAPI.AQL_Repack_Data",
                                            "Get_repack_data",
                                            Program.Client.UserToken,
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                    if(dtJson1.Rows.Count>0)
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = dtJson1;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                    }
                    
                    
                }


            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        public void Update_Repack_Data()
        {
            try
            {
                if (string.IsNullOrEmpty(textBox7.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Repack Quantity");
                    return;
                }

                if (string.IsNullOrEmpty(textBox5.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Repack reason");
                    return;
                }

                if (string.IsNullOrEmpty(txt_update.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Update Reason");
                    return;
                }
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("po", textBox6.Text);
                p.Add("prod_line", textBox8.Text);
                p.Add("repack_qty", textBox7.Text);
                p.Add("reason", textBox5.Text);
                p.Add("repackdate", dateTimePicker2.Value.ToString("yyyy/MM/dd"));
                p.Add("update_reason", txt_update.Text);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",
                                            "SJ_AQLAPI.AQL_Repack_Data",
                                            "Update_Repack_Data",
                                            Program.Client.UserToken,
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Updated Successfully");
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
                clear();
                Get_Repack_Data();
                tabControl1.SelectedIndex = 1;
                dateTimePicker2.Enabled = true;
                textBox6.ReadOnly = false;
                btn_update.Visible = false;
                btn_cancel.Visible = false;
                txt_update.Visible = false;
                lbl_update.Visible = false;
                btn_clear.Visible = true;
                btn_submit.Visible = true;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                clear();
            }
        }
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "Repack_Data.xls";
                ExportExcels.Export(a, dataGridView1);
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            Submit_Repack_Data();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            Update_Repack_Data();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            clear();
            tabControl1.SelectedIndex = 1;
            dateTimePicker2.Enabled = true;
            textBox6.ReadOnly = false;
            btn_update.Visible = false;
            btn_cancel.Visible = false;
            txt_update.Visible = false;
            lbl_update.Visible = false;
            btn_clear.Visible = true;
            btn_submit.Visible = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "DELETE")
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["LOCK_STATUS"].Value.ToString() == "Locked")
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Data Locked");
                        return;
                    }
                    DialogResult result = MessageBox.Show($@"Are you sure you want to delete the data?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {

                        try
                        {
                            string RepackDate = dataGridView1.Rows[e.RowIndex].Cells["REPACK_DATE"].Value.ToString();
                            string ProdLine = dataGridView1.Rows[e.RowIndex].Cells["PRODUCTION_LINE"].Value.ToString();
                            string Po = dataGridView1.Rows[e.RowIndex].Cells["PO"].Value.ToString();
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("RepackDate", RepackDate);
                            p.Add("ProdLine", ProdLine);
                            p.Add("Po", Po);
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",
                                            "SJ_AQLAPI.AQL_Repack_Data",
                                            "Delete_repack_data",
                                            Program.Client.UserToken,
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (ret.IsSuccess)
                            {
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Deleted Successfully");
                                Get_Repack_Data();
                            }
                            else
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to delete the data");
                                Get_Repack_Data();
                            }
                        }
                        catch (Exception ex)
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to delete the data");
                        }
                    }
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "EDIT")
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["LOCK_STATUS"].Value.ToString() == "Locked")
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Data Locked");
                        return;
                    }
                    tabControl1.SelectedIndex = 0;
                    dateTimePicker2.Text = dataGridView1.Rows[e.RowIndex].Cells["REPACK_DATE"].Value.ToString();
                    textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells["PRODUCTION_LINE"].Value.ToString();
                    textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells["PO"].Value.ToString();
                    textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells["REPACK_QTY"].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells["REPACK_REASON"].Value.ToString();
                    txt_update.Text = dataGridView1.Rows[e.RowIndex].Cells["UPDATE_REASON"].Value.ToString();

                    dateTimePicker2.Enabled = false;
                    textBox6.ReadOnly = true;
                    btn_update.Visible = true;
                    btn_cancel.Visible = true;
                    txt_update.Visible = true;
                    lbl_update.Visible = true;
                    btn_clear.Visible = false;
                    btn_submit.Visible = false;


                }
            }
        }
    } 
}
