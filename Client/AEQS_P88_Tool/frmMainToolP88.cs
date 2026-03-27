using MaterialSkin.Controls;
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
using System.IO;
using AutocompleteMenuNS;
using Newtonsoft.Json;
using DataGrid.DataGridViewCustomColumn;

namespace AEQS_P88_Tool
{
    public partial class frmMainToolP88 : MaterialForm
    {
        AutoCompleteStringCollection Autodata;
        Boolean IsExist = false;
        public frmMainToolP88()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            autocompleteMenu1.SetAutocompleteMenu(textbox5, autocompleteMenu1);
            autocompleteMenu1.SetAutocompleteMenu(textBox7, autocompleteMenu1);
            LoadUnique_Key();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        { 
            GetDataByReportType();
        }

        public void LoadUnique_Key()
        {

            textbox5.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textbox5.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox7.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox7.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> p = new Dictionary<string, string>();
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "AEQS_P88API",//class library name
                                                "AEQS_P88API.AEQS_P88_DataSync",//class name
                                                "LoadUnique_Key",//method name
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));

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
                    autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["UNIQUE_KEY"].ToString() }, dt.Rows[i]["UNIQUE_KEY"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                    n++;
                }
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            IsExist = CheckModifyUser();
            if (!IsExist)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "This User Don't have permission to Edit Data");
            }
            else
            {
                if (dataGridView9.Rows.Count > 0)
                {
                    #region //old code
                    //if (dataGridView1.Rows.Count > 0)
                    //{
                    //    dgv1editing(dataGridView1, 0);
                    //    dataGridView1.Columns[0].ReadOnly = true;
                    //    //dataGridView1.Columns[3].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[1].ReadOnly = true;
                    //    //dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[2].ReadOnly = true;
                    //    //dataGridView1.Columns[2].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[8].ReadOnly = true;
                    //    //dataGridView1.Columns[8].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[19].ReadOnly = true;
                    //    //dataGridView1.Columns[19].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[21].ReadOnly = true;
                    //    //dataGridView1.Columns[21].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[22].ReadOnly = true;
                    //    //dataGridView1.Columns[22].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[23].ReadOnly = true;
                    //    //dataGridView1.Columns[23].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[24].ReadOnly = true;
                    //    //dataGridView1.Columns[24].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[25].ReadOnly = true;
                    //    //dataGridView1.Columns[25].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[26].ReadOnly = true;
                    //    //dataGridView1.Columns[26].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[27].ReadOnly = true;
                    //    //dataGridView1.Columns[27].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[28].ReadOnly = true;
                    //    //dataGridView1.Columns[28].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[29].ReadOnly = true;
                    //    //dataGridView1.Columns[29].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[30].ReadOnly = true;
                    //    //dataGridView1.Columns[30].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[31].ReadOnly = true;
                    //    //dataGridView1.Columns[31].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[32].ReadOnly = true;
                    //    //dataGridView1.Columns[32].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[33].ReadOnly = true;
                    //    //dataGridView1.Columns[33].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[34].ReadOnly = true;
                    //    //dataGridView1.Columns[34].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[35].ReadOnly = true;
                    //    //dataGridView1.Columns[35].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[36].ReadOnly = true;
                    //    //dataGridView1.Columns[36].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[41].ReadOnly = true;
                    //    //dataGridView1.Columns[41].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[44].ReadOnly = true;
                    //    //dataGridView1.Columns[44].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[45].ReadOnly = true;
                    //    //dataGridView1.Columns[45].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //    dataGridView1.Columns[46].ReadOnly = true;
                    //    //dataGridView1.Columns[46].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    //}
                    #endregion
                    if (dataGridView9.Rows.Count > 0)  // Grid 1
                    {
                        editing(dataGridView9, 0);
                        dataGridView9.Columns[2].ReadOnly = true;
                        dataGridView9.Columns[3].ReadOnly = true;
                    }
                    if (dataGridView4.Rows.Count > 0)  // Grid 2
                    {
                        editing(dataGridView4, 0);
                        dataGridView4.Columns[1].ReadOnly = true;
                        dataGridView4.Columns[2].ReadOnly = true;
                    }
                    if (dataGridView2.Rows.Count > 0) // Grid 3
                    {
                        editing(dataGridView2, 0);
                        dataGridView2.Columns[1].ReadOnly = true;
                        dataGridView2.Columns[2].ReadOnly = true;
                        dataGridView2.Columns[3].ReadOnly = true;
                        dataGridView2.Columns[8].ReadOnly = true;
                        dataGridView2.Columns[9].ReadOnly = true;
                    }
                    if (dataGridView3.Rows.Count > 0) // Grid 4
                    {
                        editing(dataGridView3, 0);
                        dataGridView3.Columns[3].ReadOnly = true;
                        dataGridView3.Columns[1].ReadOnly = true;
                        dataGridView3.Columns[2].ReadOnly = true;
                    }

                    SJeMES_Control_Library.MessageHelper.ShowOK(this, "Editable mode is turned on。");
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0 || dataGridView3.Rows.Count > 0 || dataGridView4.Rows.Count > 0)
            {
                int a = dataGridView9.CurrentRow.Index;
                if (dataGridView9.Rows.Count > 0 || dataGridView9.Rows[a].Cells["UNIQUE_KEY2"].Value.ToString() == dataGridView2.Rows[0].Cells["UNION_ID"].Value.ToString())
                {
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();
                    DataTable dt4 = new DataTable();

                    //foreach (DataGridViewRow row in dataGridView1.Rows)
                    //    bool isSelected = Convert.ToBoolean(row.Cells["checkBoxColumn"].Value);
                    //if (isSelected)

                    //datagridview1 to dt1
                    if (dataGridView9.Rows.Count > 0)
                    {
                        foreach (DataGridViewColumn column in dataGridView9.Columns)
                            dt1.Columns.Add(column.Name);


                        foreach (DataGridViewRow row in dataGridView9.Rows)
                        {
                            bool isSelected = Convert.ToBoolean(row.Cells["chk2"].Value);
                            if (isSelected)
                            {
                                DataRow dRow = dt1.NewRow();
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    dRow[cell.ColumnIndex] = cell.Value;
                                }
                                dt1.Rows.Add(dRow);

                            }
                        }


                        //dt1.Rows.Add();
                        //for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        //{
                        //    dt1.Rows[0][j] = dataGridView1.Rows[a].Cells[j].Value;
                        //}
                    }



                    //datagridview2 to dt2
                    if (dataGridView2.Rows.Count > 0)
                    {
                        foreach (DataGridViewColumn col in dataGridView2.Columns)
                            dt2.Columns.Add(col.Name);

                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            bool isSelected = Convert.ToBoolean(row.Cells["check2"].Value);
                            if (isSelected)
                            {
                                DataRow dRow = dt2.NewRow();
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    dRow[cell.ColumnIndex] = cell.Value;
                                }
                                dt2.Rows.Add(dRow);

                            }
                        }
                    }

                    //datagridview3 to dt3
                    if (dataGridView3.Rows.Count > 0)
                    {
                        foreach (DataGridViewColumn col in dataGridView3.Columns)
                            dt3.Columns.Add(col.Name);


                        foreach (DataGridViewRow row in dataGridView3.Rows)
                        {
                            bool isSelected = Convert.ToBoolean(row.Cells["check3"].Value);
                            if (isSelected)
                            {
                                DataRow dRow = dt3.NewRow();
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    dRow[cell.ColumnIndex] = cell.Value;
                                }
                                dt3.Rows.Add(dRow);
                            }
                        }
                    }

                    //datagridview4 to dt4
                    if (dataGridView4.Rows.Count > 0)
                    {
                        foreach (DataGridViewColumn col in dataGridView4.Columns)
                            dt4.Columns.Add(col.Name);

                        foreach (DataGridViewRow row in dataGridView4.Rows)
                        {
                            bool isSelected = Convert.ToBoolean(row.Cells["check4"].Value);
                            if (isSelected)
                            {
                                DataRow dRow = dt4.NewRow();
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    dRow[cell.ColumnIndex] = cell.Value;
                                }
                                dt4.Rows.Add(dRow);
                            }
                        }
                    }
                    if (dt1.Rows.Count > 0 || dt2.Rows.Count > 0 || dt3.Rows.Count > 0 || dt4.Rows.Count > 0)
                    {
                        Dictionary<string, Object> p = new Dictionary<string, object>();
                        p.Add("report_type_id", Convert.ToInt16(comboBox1.Text.Split('|')[0]));
                        p.Add("dt1", dt1);
                        p.Add("dt2", dt2);
                        p.Add("dt3", dt3);
                        p.Add("dt4", dt4);
                        string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "AEQS_P88API", "AEQS_P88API.AEQS_P88_DataSync", "EditItem", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        {
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Success!");
                        }
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                        }

                        dataGridView2.Rows.Clear();
                        dataGridView3.Rows.Clear();
                        dataGridView4.Rows.Clear();
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select the rows to update");
                    }


                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Mismatch Between Left and Right Table, Please check!.");
                }
            }
        }

        public void GetDataByReportType()
        {
            try
            {
                if (comboBox1.SelectedIndex != -1)
                {
                    dataGridView9.Rows.Clear();
                    dataGridView2.Rows.Clear();
                    dataGridView3.Rows.Clear();
                    dataGridView4.Rows.Clear();
                    //Request data display of api
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    //key value pair pass value
                    p.Add("from", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
                    p.Add("to", dateTimePicker2.Value.ToString("yyyy/MM/dd"));
                    p.Add("report_type_id", Convert.ToInt16(comboBox1.Text.Split('|')[0]));
                    p.Add("po", textBox1.Text);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "AEQS_P88API",//class library name
                                                "AEQS_P88API.AEQS_P88_DataSync",//class name
                                                "GetDataByReportType",//method name
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                    if (!ret.IsSuccess)
                    {
                        throw new Exception(ret.ErrMsg);
                    }

                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    //view data display
                    //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                    var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    //dataGridView1.DataSource = dt;
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView9.Rows.Add();
                            DataGridViewRow dgvr = dataGridView9.Rows[i];
                            dgvr.Cells["UNIQUE_KEY2"].Value = dr["UNIQUE_KEY"].ToString();
                            dgvr.Cells["STATUS2"].Value = dr["STATUS"].ToString();
                            dgvr.Cells["DATE_STARTED2"].Value = dr["DATE_STARTED"].ToString();
                            dgvr.Cells["DEFECTIVE_PARTS2"].Value = dr["DEFECTIVE_PARTS"].ToString();
                            dgvr.Cells["PASSFAILS_0_TITLE2"].Value = dr["PASSFAILS_0_TITLE"].ToString();
                            dgvr.Cells["PASSFAILS_0_TYPE2"].Value = dr["PASSFAILS_0_TYPE"].ToString();
                            dgvr.Cells["PASSFAILS_0_SUBSECTION2"].Value = dr["PASSFAILS_0_SUBSECTION"].ToString();
                            dgvr.Cells["PASSFAILS_0_LISTVALUES_VALUE2"].Value = dr["PASSFAILS_0_LISTVALUES_VALUE"].ToString();
                            dgvr.Cells["MODIFY_COUNT2"].Value = dr["MODIFY_COUNT"].ToString();
                            //dgvr.Cells["INSERT_DATE"].Value = dr["INSERT_DATE"].ToString();
                            dgvr.Cells["IS_SYNC2"].Value = dr["IS_SYNC"].ToString();
                            dgvr.Cells["STATUS_CODE2"].Value = dr["STATUS_CODE"].ToString();
                            dataGridView9.Rows[i].ReadOnly = true;
                            i++;
                            //Application.DoEvents();//转让控制权
                        }
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Report Type!");
                }

            }
            catch (Exception ex)
            {
                //this.Enabled = true;
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void editing(DataGridView dg, int bjzt)
        {
            if (bjzt == 0)
            {
                for (int i = 0; i < dg.Rows.Count; i++)
                {
                    dg.Rows[i].ReadOnly = false; //Enable Edit
                }
            }
            else
            {
                for (int i = 0; i < dg.Rows.Count; i++)
                {
                    dg.Rows[i].ReadOnly = true; // Disable Edit
                }
            }
        }

        private void dataGridView9_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dataGridView9.Columns[e.ColumnIndex].Name == "delete")
                {
                    DialogResult dr = MessageBox.Show("Are you sure you want to delete!", "Prompt", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dr == DialogResult.OK)
                    {
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        string UNIQUE_KEY = dataGridView9.Rows[e.RowIndex].Cells["UNIQUE_KEY2"].Value.ToString();
                        string STATUS = dataGridView9.Rows[e.RowIndex].Cells["STATUS2"].Value.ToString();
                        string DATE_STARTED = dataGridView9.Rows[e.RowIndex].Cells["DATE_STARTED2"].Value.ToString();

                        string DEFECTIVE_PARTS = dataGridView9.Rows[e.RowIndex].Cells["DEFECTIVE_PARTS2"].Value.ToString();
                        string PASSFAILS_0_TITLE = dataGridView9.Rows[e.RowIndex].Cells["PASSFAILS_0_TITLE2"].Value.ToString();
                        string PASSFAILS_0_SUBSECTION = dataGridView9.Rows[e.RowIndex].Cells["PASSFAILS_0_SUBSECTION2"].Value.ToString();
                        string PASSFAILS_0_LISTVALUES_VALUE = dataGridView9.Rows[e.RowIndex].Cells["PASSFAILS_0_LISTVALUES_VALUE2"].Value.ToString();

                        p.Add("UNIQUE_KEY", UNIQUE_KEY);
                        p.Add("STATUS", STATUS);
                        p.Add("DATE_STARTED", DATE_STARTED);
                        p.Add("DEFECTIVE_PARTS", DEFECTIVE_PARTS);

                        p.Add("PASSFAILS_0_TITLE", PASSFAILS_0_TITLE);
                        p.Add("PASSFAILS_0_SUBSECTION", PASSFAILS_0_SUBSECTION);
                        p.Add("PASSFAILS_0_LISTVALUES_VALUE", PASSFAILS_0_LISTVALUES_VALUE);

                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                    "AEQS_P88API",//类库名
                                                    "AEQS_P88API.AEQS_P88_DataSync",//类名
                                                    "DeleteDataByUniqueKey",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (ret.IsSuccess)
                        {
                            MessageBox.Show("successfully deleted");
                            GetDataByReportType();
                        }
                    }
                }
                else
                {

                    dataGridView9.Rows[e.RowIndex].Cells["chk2"].Value = "true";

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        if (row.Index != e.RowIndex)
                        {
                            dataGridView9.Rows[row.Index].Cells["chk2"].Value = "false";
                        }
                    }
                    //dataGridView1.Rows[e.RowIndex].Cells["chk"].Value = "true";
                }

            }
        }

        private void dataGridView9_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView9.Rows.Count > 0)
            {
                editing(dataGridView9, 1);
                dataGridView9.Columns[8].ReadOnly = false;
                try
                {
                    //foreach (DataGridViewRow row in dataGridView1.Rows)
                    //{
                    //    row.DefaultCellStyle.BackColor = Color.White;
                    //}
                    dataGridView2.Rows.Clear();
                    dataGridView3.Rows.Clear();
                    dataGridView4.Rows.Clear();
                    int a = dataGridView9.CurrentRow.Index;
                    //dataGridView1.Rows[a].DefaultCellStyle.BackColor = Color.SkyBlue;
                    string h = dataGridView9.Rows[a].Cells["UNIQUE_KEY2"].Value.ToString();
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("vSource", h);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "AEQS_P88API",//class library name
                                                "AEQS_P88API.AEQS_P88_DataSync",//class name
                                                "GetDataByUniqueKey",//method name
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        throw new Exception(ret.ErrMsg);
                    }

                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    var dtDgv2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());
                    if (dtDgv2.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dtDgv2.Rows)
                        {
                            dataGridView2.Rows.Add();
                            DataGridViewRow dgvr = dataGridView2.Rows[i];
                            dgvr.Cells["ID"].Value = dr["ID"].ToString();
                            dgvr.Cells["UNION_ID"].Value = dr["UNION_ID"].ToString();
                            dgvr.Cells["SECTIONS_TYPE"].Value = dr["SECTIONS_TYPE"].ToString();
                            dgvr.Cells["SECTIONS_TITLE"].Value = dr["SECTIONS_TITLE"].ToString();
                            dgvr.Cells["SECTIONS_RESULT_ID"].Value = dr["SECTIONS_RESULT_ID"].ToString();
                            dgvr.Cells["SECTIONS_QTY_INSPECTED"].Value = dr["SECTIONS_QTY_INSPECTED"].ToString();
                            dgvr.Cells["SECTIONS_SAMPLED_INSPECTED"].Value = dr["SECTIONS_SAMPLED_INSPECTED"].ToString();
                            dgvr.Cells["SECTIONS_DEFECTIVE_PARTS"].Value = dr["SECTIONS_DEFECTIVE_PARTS"].ToString();
                            dgvr.Cells["SECTIONS_INSPECTION_LEVEL"].Value = dr["SECTIONS_INSPECTION_LEVEL"].ToString();
                            dgvr.Cells["SECTIONS_INSPECTION_METHOD"].Value = dr["SECTIONS_INSPECTION_METHOD"].ToString();
                            dgvr.Cells["SECTIONS_AQL_MINOR"].Value = dr["SECTIONS_AQL_MINOR"].ToString();
                            dgvr.Cells["SECTIONS_AQL_MAJOR"].Value = dr["SECTIONS_AQL_MAJOR"].ToString();
                            dgvr.Cells["SECTIONS_AQL_CRITICAL"].Value = dr["SECTIONS_AQL_CRITICAL"].ToString();
                            dgvr.Cells["SECTIONS_BARCODES_VALUE"].Value = dr["SECTIONS_BARCODES_VALUE"].ToString();
                            dgvr.Cells["SECTIONS_QTY_TYPE"].Value = dr["SECTIONS_QTY_TYPE"].ToString();
                            dgvr.Cells["SECTIONS_MAX_MINOR_DEFECTS"].Value = dr["SECTIONS_MAX_MINOR_DEFECTS"].ToString();
                            dgvr.Cells["SECTIONS_MAX_MAJOR_DEFECTS"].Value = dr["SECTIONS_MAX_MAJOR_DEFECTS"].ToString();
                            dgvr.Cells["SECTIONS_MAX_MAJOR_A_DEFECTS"].Value = dr["SECTIONS_MAX_MAJOR_A_DEFECTS"].ToString();
                            dgvr.Cells["SECTIONS_MAX_MAJOR_B_DEFECTS"].Value = dr["SECTIONS_MAX_MAJOR_B_DEFECTS"].ToString();
                            dgvr.Cells["SECTIONS_MAX_CRITICAL_DEFECTS"].Value = dr["SECTIONS_MAX_CRITICAL_DEFECTS"].ToString();
                            dgvr.Cells["SECTIONS_DEFECTS_LABEL"].Value = dr["SECTIONS_DEFECTS_LABEL"].ToString();
                            dgvr.Cells["SECTIONS_DEFECTS_SUBSECTION"].Value = dr["SECTIONS_DEFECTS_SUBSECTION"].ToString();
                            dgvr.Cells["SECTIONS_DEFECTS_CODE"].Value = dr["SECTIONS_DEFECTS_CODE"].ToString();
                            dgvr.Cells["SECTIONS_DEFECTS_CRITICAL_LEVEL"].Value = dr["SECTIONS_DEFECTS_CRITICAL_LEVEL"].ToString();
                            dgvr.Cells["SECTIONS_DEFECTS_MAJOR_LEVEL"].Value = dr["SECTIONS_DEFECTS_MAJOR_LEVEL"].ToString();
                            dgvr.Cells["SECTIONS_DEFECTS_MINOR_LEVEL"].Value = dr["SECTIONS_DEFECTS_MINOR_LEVEL"].ToString();
                            dgvr.Cells["SECTIONS_DEFECTS_COMMENTS"].Value = dr["SECTIONS_DEFECTS_COMMENTS"].ToString();
                            dataGridView2.Rows[i].ReadOnly = true;
                            i++;
                            //Application.DoEvents();//转让控制权
                        }
                    }

                    var dtDgv3 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
                    if (dtDgv3.Rows.Count > 0)
                    {
                        int j = 0;
                        foreach (DataRow dr in dtDgv3.Rows)
                        {
                            dataGridView3.Rows.Add();
                            DataGridViewRow dgvr = dataGridView3.Rows[j];
                            dgvr.Cells["ID1"].Value = dr["ID"].ToString();
                            dgvr.Cells["UNION_ID1"].Value = dr["UNION_ID"].ToString();
                            dgvr.Cells["PASSFAILS_TITLE"].Value = dr["PASSFAILS_TITLE"].ToString();
                            dgvr.Cells["PASSFAILS_VALUE"].Value = dr["PASSFAILS_VALUE"].ToString();
                            dgvr.Cells["PASSFAILS_TYPE"].Value = dr["PASSFAILS_TYPE"].ToString();
                            dgvr.Cells["PASSFAILS_SUBSECTION"].Value = dr["PASSFAILS_SUBSECTION"].ToString();
                            dgvr.Cells["PASSFAILS_CHECKLISTSUBSECTION"].Value = dr["PASSFAILS_CHECKLISTSUBSECTION"].ToString();
                            dgvr.Cells["PASSFAILS_STATUS"].Value = dr["PASSFAILS_STATUS"].ToString();
                            dgvr.Cells["PASSFAILS_COMMENT"].Value = dr["PASSFAILS_COMMENT"].ToString();
                            dataGridView3.Rows[j].ReadOnly = true;
                            j++;
                            //Application.DoEvents();//转让控制权
                        }
                    }

                    var dtDgv4 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data3"].ToString());
                    if (dtDgv4.Rows.Count > 0)
                    {
                        int k = 0;
                        foreach (DataRow dr in dtDgv4.Rows)
                        {
                            dataGridView4.Rows.Add();
                            DataGridViewRow dgvr = dataGridView4.Rows[k];
                            dgvr.Cells["ID2"].Value = dr["ID"].ToString();
                            dgvr.Cells["UNION_ID_A"].Value = dr["UNION_ID"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_SAMPLED_INSPECTED"].Value = dr["ASSIGNMENT_ITEMS_SAMPLED_INSPECTED"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_INSPECTION_RESULT_ID"].Value = dr["ASSIGNMENT_ITEMS_INSPECTION_RESULT_ID"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_INSPECTION_STATUS_ID"].Value = dr["ASSIGNMENT_ITEMS_INSPECTION_STATUS_ID"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_QTY_INSPECTED"].Value = dr["ASSIGNMENT_ITEMS_QTY_INSPECTED"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_INSPECTION_COMPLETED_DATE"].Value = dr["ASSIGNMENT_ITEMS_INSPECTION_COMPLETED_DATE"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_TOTAL_INSPECTION_MINUTES"].Value = dr["ASSIGNMENT_ITEMS_TOTAL_INSPECTION_MINUTES"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_SAMPLING_SIZE"].Value = dr["ASSIGNMENT_ITEMS_SAMPLING_SIZE"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_QTY_TO_INSPECT"].Value = dr["ASSIGNMENT_ITEMS_QTY_TO_INSPECT"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_AQL_MINOR"].Value = dr["ASSIGNMENT_ITEMS_AQL_MINOR"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_AQL_MAJOR"].Value = dr["ASSIGNMENT_ITEMS_AQL_MAJOR"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_AQL_MAJOR_A"].Value = dr["ASSIGNMENT_ITEMS_AQL_MAJOR_A"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_AQL_MAJOR_B"].Value = dr["ASSIGNMENT_ITEMS_AQL_MAJOR_B"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_AQL_CRITICAL"].Value = dr["ASSIGNMENT_ITEMS_AQL_CRITICAL"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_SUPPLIER_BOOKING_MSG"].Value = dr["ASSIGNMENT_ITEMS_SUPPLIER_BOOKING_MSG"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_CONCLUSION_REMARKS"].Value = dr["ASSIGNMENT_ITEMS_CONCLUSION_REMARKS"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_ASSIGNMENT_REPORT_TYPE_ID"].Value = dr["ASSIGNMENT_ITEMS_ASSIGNMENT_REPORT_TYPE_ID"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_ASSIGNMENT_INSPECTOR_USERNAME"].Value = dr["ASSIGNMENT_ITEMS_ASSIGNMENT_INSPECTOR_USERNAME"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_ASSIGNMENT_DATE_INSPECTION"].Value = dr["ASSIGNMENT_ITEMS_ASSIGNMENT_DATE_INSPECTION"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_ASSIGNMENT_INSPECTION_LEVEL"].Value = dr["ASSIGNMENT_ITEMS_ASSIGNMENT_INSPECTION_LEVEL"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_ASSIGNMENT_INSPECTION_METHOD"].Value = dr["ASSIGNMENT_ITEMS_ASSIGNMENT_INSPECTION_METHOD"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_PO_LINE_QTY"].Value = dr["ASSIGNMENT_ITEMS_PO_LINE_QTY"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_PO_LINE_ETD"].Value = dr["ASSIGNMENT_ITEMS_PO_LINE_ETD"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_PO_LINE_ETA"].Value = dr["ASSIGNMENT_ITEMS_PO_LINE_ETA"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_PO_LINE_COLOR"].Value = dr["ASSIGNMENT_ITEMS_PO_LINE_COLOR"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_PO_LINE_SIZE"].Value = dr["ASSIGNMENT_ITEMS_PO_LINE_SIZE"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_PO_LINE_STYLE"].Value = dr["ASSIGNMENT_ITEMS_PO_LINE_STYLE"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_PO_LINE_PO_EXPORTER_ID"].Value = dr["ASSIGNMENT_ITEMS_PO_LINE_PO_EXPORTER_ID"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_PO_LINE_PO_EXPORTER_ERP_BUSINESS_ID"].Value = dr["ASSIGNMENT_ITEMS_PO_LINE_PO_EXPORTER_ERP_BUSINESS_ID"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_PO_LINE_PO_NUMBER"].Value = dr["ASSIGNMENT_ITEMS_PO_LINE_PO_NUMBER"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_PO_LINE_CUSTOMER_PO"].Value = dr["ASSIGNMENT_ITEMS_PO_LINE_CUSTOMER_PO"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_PO_LINE_IMPORTER_ID"].Value = dr["ASSIGNMENT_ITEMS_PO_LINE_IMPORTER_ID"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_PO_LINE_IMPORTER_ERP_BUSINESS_ID"].Value = dr["ASSIGNMENT_ITEMS_PO_LINE_IMPORTER_ERP_BUSINESS_ID"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_PO_LINE_IMPORTER_PROJECT_ID"].Value = dr["ASSIGNMENT_ITEMS_PO_LINE_IMPORTER_PROJECT_ID"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_PO_LINE_SKU_SKU_NUMBER"].Value = dr["ASSIGNMENT_ITEMS_PO_LINE_SKU_SKU_NUMBER"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_PO_LINE_SKU_ITEM_NAME"].Value = dr["ASSIGNMENT_ITEMS_PO_LINE_SKU_ITEM_NAME"].ToString();
                            dgvr.Cells["ASSIGNMENT_ITEMS_PO_LINE_SKU_ITEM_DESCRIPTION"].Value = dr["ASSIGNMENT_ITEMS_PO_LINE_SKU_ITEM_DESCRIPTION"].ToString();
                            dataGridView4.Rows[k].ReadOnly = true;
                            k++;
                            //Application.DoEvents();//转让控制权
                        }
                    }
                }
                catch (Exception ex)
                {
                    dataGridView2.DataSource = null;
                    dataGridView3.DataSource = null;
                    //this.Enabled = true;
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
            }
        }

        private void btnsearch2_Click(object sender, EventArgs e)
        {
            GetSyncData();
        }

        public void GetSyncData()
        {
            try
            {
                if (comboBox4.SelectedIndex != -1)
                {
                    dataGridView5.Rows.Clear();
                    //dataGridView2.Rows.Clear();
                    //dataGridView3.Rows.Clear();
                    //dataGridView4.Rows.Clear();
                    //Request data display of api
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    //key value pair pass value
                    p.Add("unique_key", textbox5.Text);
                    p.Add("po", textBox4.Text);
                    p.Add("is_sync", comboBox3.Text);
                    p.Add("report_type_id", Convert.ToInt16(comboBox4.Text.Split('|')[0]));
                    p.Add("from", dateTimePicker3.Value.ToString("yyyy/MM/dd"));
                    p.Add("to", dateTimePicker4.Value.ToString("yyyy/MM/dd"));
                    //p.Add("report_type_id", Convert.ToInt16(comboBox1.Text.Split('|')[0]));
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "AEQS_P88API",//class library name
                                                "AEQS_P88API.AEQS_P88_DataSync",//class name
                                                "GetSyncData",//method name
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                    if (!ret.IsSuccess)
                    {
                        throw new Exception(ret.ErrMsg);
                    }

                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    //view data display
                    //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                    var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    //dataGridView1.DataSource = dt;
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView5.Rows.Add();
                            DataGridViewRow dgvr = dataGridView5.Rows[i];
                            dgvr.Cells["UNIQUE_KEY3"].Value = dr["UNIQUE_KEY"].ToString();
                            dgvr.Cells["STATUS3"].Value = dr["STATUS"].ToString();
                            dgvr.Cells["DATE_STARTED3"].Value = dr["DATE_STARTED"].ToString();
                            dgvr.Cells["DEFECTIVE_PARTS3"].Value = dr["DEFECTIVE_PARTS"].ToString();
                            dgvr.Cells["PASSFAILS_0_TITLE3"].Value = dr["PASSFAILS_0_TITLE"].ToString();
                            dgvr.Cells["PASSFAILS_0_TYPE3"].Value = dr["PASSFAILS_0_TYPE"].ToString();
                            dgvr.Cells["PASSFAILS_0_SUBSECTION3"].Value = dr["PASSFAILS_0_SUBSECTION"].ToString();
                            dgvr.Cells["PASSFAILS_0_LISTVALUES_VALUE3"].Value = dr["PASSFAILS_0_LISTVALUES_VALUE"].ToString();
                            dgvr.Cells["MODIFY_COUNT3"].Value = dr["MODIFY_COUNT"].ToString();
                            dgvr.Cells["SYNC_DATE"].Value = dr["SYNC_DATE"].ToString();
                            dgvr.Cells["IS_SYNC3"].Value = dr["IS_SYNC"].ToString();
                            dgvr.Cells["STATUS_CODE3"].Value = dr["STATUS_CODE"].ToString();
                            dataGridView5.Rows[i].ReadOnly = true;
                            i++;
                            //Application.DoEvents();//转让控制权
                        }
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Report Type!");
                }

            }
            catch (Exception ex)
            {
                //this.Enabled = true;
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void dataGridView5_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex > -1)
            {
                if (dataGridView5.Columns[e.ColumnIndex].Name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView5.Rows[this.dataGridView5.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    string currentItem = cell.CurrentItem;
                    if (currentItem == null)
                    {
                        return;
                    }
                    else
                    {
                        string Json = string.Empty;
                        string IS_SYNC = dataGridView5.Rows[e.RowIndex].Cells["IS_SYNC3"].Value.ToString();
                        string UNIQUE_KEY = dataGridView5.Rows[e.RowIndex].Cells["UNIQUE_KEY3"].Value.ToString();
                        string SYNC_DATE = dataGridView5.Rows[e.RowIndex].Cells["SYNC_DATE"].Value.ToString();
                        //string operation = comboBox4.SelectedValue.ToString();
                        string operation = comboBox4.Text.Split('|')[1].ToString();

                        string uploadUrl = Program.Client.UploadUrl;
                        Uri uri = new Uri(uploadUrl);
                        // Get host name and port number
                        uploadUrl = uri.GetLeftPart(UriPartial.Authority);

                        //string file_urls = uploadUrl + @"//P88_FMS/Log/Platform/" + operation + "/fmslog_" + SYNC_DATE + ".txt";

                        string file_urls = uploadUrl + @"//wwwroot/AEQS_To_Pivot88_Data_Sync/Debug/Log/Platform/" + operation + "/fmslog_" + SYNC_DATE + ".txt";

                        string loadPath = System.Environment.CurrentDirectory + @"\openFile_MiddleTool\" + operation + "";

                        DateTime currTime = DateTime.Now.AddDays(-7);

                        if (Directory.Exists(loadPath))
                        {
                            foreach (string d in Directory.GetFileSystemEntries(loadPath))
                            {
                                DateTime createTime = File.GetCreationTime(d);
                                if (createTime <= currTime)
                                    File.Delete(d);
                            }
                        }
                        else
                        {
                            Directory.CreateDirectory(loadPath);
                        }
                        string filename = loadPath + @"\" + file_urls.Substring(file_urls.Replace(@"/", @"\").LastIndexOf(@"\") + 1);
                        try
                        {
                            if (!File.Exists(filename))
                            {
                                System.Net.ServicePointManager.DefaultConnectionLimit = 100;
                                System.Net.WebClient webclient = new System.Net.WebClient();
                                webclient.Credentials = System.Net.CredentialCache.DefaultCredentials;
                                //Download to local
                                webclient.DownloadFile(file_urls, filename);
                                webclient.Dispose();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Server:File does not exist");
                            return;
                        }

                        /*******    End Add by Venkat 2024/04/13    **********/


                        if (File.Exists(filename))
                        {
                            // Open the file for reading
                            using (StreamReader sr = new StreamReader(filename))
                            {
                                // Define the search condition
                                string searchTerm = UNIQUE_KEY;

                                // Read the file line by line until the end
                                string line;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    // Check if the line contains the search term
                                    if (line.Contains(searchTerm))
                                    {
                                        Json = line;
                                        break;

                                    }
                                }
                            }

                            //Write JSON data to file
                            // Find the beginning and end of the JSON string
                            if (string.IsNullOrEmpty(Json))
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, "no data！"); 
                                return;
                            }

                            string reportJson = Json;
                            int jsonStart = reportJson.IndexOf('{', reportJson.IndexOf("[JSON") + 1);
                            int jsonEnd = reportJson.LastIndexOf('}');

                            if (jsonStart == -1 || jsonEnd <= jsonStart)
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Invalid format, JSON start not found."); 
                                return;
                            }

                            //Extract JSON string
                            reportJson = reportJson.Substring(jsonStart, jsonEnd - jsonStart + 1);



                            //Check
                            if (currentItem.Equals("view"))
                            {

                                try
                                {
                                    // Try deserializing the extracted string into a JSON object
                                    dynamic jsonObject = JsonConvert.DeserializeObject(reportJson);
                                    reportJson = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

                                    JsonMessage JM = new JsonMessage(reportJson);
                                    JM.Show();
                                }
                                catch (Exception ex)
                                {
                                    SJeMES_Control_Library.MessageHelper.ShowErr(this, $"View report failed：{ex.Message}");
                                    //MessageBox.Show($"查看报告失败：{ex.Message}");
                                }

                            }
                            //download
                            else if (currentItem.Equals("download"))
                            {
                                string reportName = UNIQUE_KEY;

                                // Check if the report name is empty
                                if (string.IsNullOrEmpty(reportName))
                                {
                                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter a report name!"); 
                                    return;
                                }

                                if (MessageBox.Show("Are you sure you want to save the report?？", "Confirm to save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    // Define desktop path
                                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                                    // full file path
                                    string filePath = Path.Combine(desktopPath, $"{reportName}.json");

                                    try
                                    {
                                        // Try deserializing the extracted string into a JSON object
                                        dynamic jsonObject = JsonConvert.DeserializeObject(reportJson);
                                        string formattedJson = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                                        File.WriteAllText(filePath, formattedJson);
                                        MessageBox.Show($"Report successfully saved as {filePath}");
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show($"Failed to save report：{ex.Message}");
                                    }
                                }


                            }
                        }
                        else
                        {
                            Console.WriteLine("Local:File does not exist.");
                        }

                    }



                }
            }
        }

        public Boolean CheckModifyUser()
        {
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                               Program.Client.APIURL,
                                               "AEQS_P88API",//class library name
                                               "AEQS_P88API.AEQS_P88_DataSync",//class name
                                               "CheckModifyUser",//method name
                                               Program.Client.UserToken,//token
                                               Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            return ret.IsSuccess;
        }

        public string GetFormatJson(string UNIQUE_KEY, string Json)
        {
            // Get the report name, assuming it is stored in textBoxReportName
            string reportName = UNIQUE_KEY;
            string reportJson = Json;

            // Define desktop path
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // full file path
            string filePath = Path.Combine(desktopPath, $"{reportName}.json");

            try
            {
                //Write JSON data to file
                // Find the beginning and end of the JSON string
                int jsonStart = reportJson.IndexOf('{', reportJson.IndexOf("[JSON") + 1);
                int jsonEnd = reportJson.LastIndexOf('}');
                if (jsonStart == -1 || jsonEnd <= jsonStart)
                {
                    MessageBox.Show("Invalid format, JSON start not found.");
                    return null;
                }

                // Extract JSON string
                reportJson = reportJson.Substring(jsonStart, jsonEnd - jsonStart + 1);
                // Try deserializing the extracted string into a JSON object
                dynamic jsonObject = JsonConvert.DeserializeObject(reportJson);
                string formattedJson = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                File.WriteAllText(filePath, formattedJson);
                MessageBox.Show($"Report successfully saved as {filePath}");

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save report：{ex.Message}");
            }

            return reportJson;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView6.DataSource = null;
            dataGridView8.DataSource = null;
            dataGridView10.DataSource = null;
            dataGridView11.DataSource = null; 
            Dictionary<string, object> p = new Dictionary<string, object>(); 
            p.Add("unique_key", textBox7.Text);  
            //p.Add("report_type_id", Convert.ToInt16(comboBox5.Text.Split('|')[0]));
            p.Add("from", dateTimePicker5.Value.ToString("yyyy/MM/dd"));
            p.Add("to", dateTimePicker6.Value.ToString("yyyy/MM/dd")); 
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "AEQS_P88API",//class library name
                                        "AEQS_P88API.AEQS_P88_DataSync",//class name
                                        "GetLogData",//method name
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (!ret.IsSuccess)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                return;
            }

            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //view data display
            //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
            var dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());
            var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
            var dt3 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data3"].ToString());
            var dt4 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data4"].ToString());

            dataGridView6.DataSource = dt1;
            dataGridView8.DataSource = dt2;
            dataGridView10.DataSource = dt3;
            dataGridView11.DataSource = dt4;
        }
    }
}
