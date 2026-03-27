using MaterialSkin;
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

namespace AEQS_P88_Tool
{

    public partial class FrmToolMain : MaterialForm
    {

        public FrmToolMain()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            //materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            //Program.SkinThemes, materialSkinManager, this);
        }

        private void FrmToolMain_Load(object sender, EventArgs e)
        {
            //this.dataGridView1.AutoGenerateColumns = false;

            //foreach (DataGridViewColumn column in this.dataGridView2.Columns)
            //{
            //    column.ReadOnly = column.Index == 0 ? false : true;
            //}
            //this.dataGridView2.ReadOnly = true;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            //this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetDataByReportType();
            #region Search
            //try
            //{
            //    if (comboBox1.SelectedIndex != -1)
            //    {
            //        dataGridView1.Rows.Clear();
            //        dataGridView2.Rows.Clear();
            //        dataGridView3.Rows.Clear();
            //        dataGridView4.Rows.Clear();
            //        //Request data display of api
            //        Dictionary<string, object> p = new Dictionary<string, object>();
            //        //key value pair pass value
            //        p.Add("from", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
            //        p.Add("to", dateTimePicker2.Value.ToString("yyyy/MM/dd"));
            //        p.Add("report_type_id", Convert.ToInt16(comboBox1.Text.Split('|')[0]));
            //        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
            //                                    Program.Client.APIURL,
            //                                    "AEQS_P88API",//class library name
            //                                    "AEQS_P88API.AEQS_P88_DataSync",//class name
            //                                    "GetDataByReportType",//method name
            //                                    Program.Client.UserToken,//token
            //                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
            //        //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            //        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            //        if (!ret.IsSuccess)
            //        {
            //            throw new Exception(ret.ErrMsg);
            //        }

            //        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //        //view data display
            //        //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
            //        var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            //        //dataGridView1.DataSource = dt;
            //        if (dt.Rows.Count > 0)
            //        {
            //            int i = 0;
            //            foreach (DataRow dr in dt.Rows)
            //            {
            //                dataGridView1.Rows.Add();
            //                DataGridViewRow dgvr = dataGridView1.Rows[i];
            //                dgvr.Cells["UNIQUE_KEY"].Value = dr["UNIQUE_KEY"].ToString();
            //                dgvr.Cells["STATUS"].Value = dr["STATUS"].ToString();
            //                dgvr.Cells["DATE_STARTED"].Value = dr["DATE_STARTED"].ToString();
            //                dgvr.Cells["DEFECTIVE_PARTS"].Value = dr["DEFECTIVE_PARTS"].ToString(); 
            //                dgvr.Cells["PASSFAILS_0_TITLE"].Value = dr["PASSFAILS_0_TITLE"].ToString();
            //                dgvr.Cells["PASSFAILS_0_TYPE"].Value = dr["PASSFAILS_0_TYPE"].ToString();
            //                dgvr.Cells["PASSFAILS_0_SUBSECTION"].Value = dr["PASSFAILS_0_SUBSECTION"].ToString();
            //                dgvr.Cells["PASSFAILS_0_LISTVALUES_VALUE"].Value = dr["PASSFAILS_0_LISTVALUES_VALUE"].ToString();
            //                dgvr.Cells["MODIFY_COUNT"].Value = dr["MODIFY_COUNT"].ToString();
            //                //dgvr.Cells["INSERT_DATE"].Value = dr["INSERT_DATE"].ToString();
            //                //dgvr.Cells["STATUS_CODE"].Value = dr["STATUS_CODE"].ToString();
            //                dataGridView1.Rows[i].ReadOnly = true;
            //                i++;
            //                //Application.DoEvents();//转让控制权
            //            }
            //        }
            //    }
            //    else
            //    {
            //        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Report Type!");
            //    }

            //}
            //catch (Exception ex)
            //{
            //    //this.Enabled = true;
            //    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            //}
            #endregion
        }

        public void GetDataByReportType()
        {
            try
            {
                if (comboBox1.SelectedIndex != -1)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView2.Rows.Clear();
                    dataGridView3.Rows.Clear();
                    dataGridView4.Rows.Clear();
                    //Request data display of api
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    //key value pair pass value
                    p.Add("from", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
                    p.Add("to", dateTimePicker2.Value.ToString("yyyy/MM/dd"));
                    p.Add("report_type_id", Convert.ToInt16(comboBox1.Text.Split('|')[0]));
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
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["UNIQUE_KEY"].Value = dr["UNIQUE_KEY"].ToString();
                            dgvr.Cells["STATUS"].Value = dr["STATUS"].ToString();
                            dgvr.Cells["DATE_STARTED"].Value = dr["DATE_STARTED"].ToString();
                            dgvr.Cells["DEFECTIVE_PARTS"].Value = dr["DEFECTIVE_PARTS"].ToString();
                            dgvr.Cells["PASSFAILS_0_TITLE"].Value = dr["PASSFAILS_0_TITLE"].ToString();
                            dgvr.Cells["PASSFAILS_0_TYPE"].Value = dr["PASSFAILS_0_TYPE"].ToString();
                            dgvr.Cells["PASSFAILS_0_SUBSECTION"].Value = dr["PASSFAILS_0_SUBSECTION"].ToString();
                            dgvr.Cells["PASSFAILS_0_LISTVALUES_VALUE"].Value = dr["PASSFAILS_0_LISTVALUES_VALUE"].ToString();
                            dgvr.Cells["MODIFY_COUNT"].Value = dr["MODIFY_COUNT"].ToString();
                            //dgvr.Cells["INSERT_DATE"].Value = dr["INSERT_DATE"].ToString();
                            dgvr.Cells["IS_SYNC"].Value = dr["IS_SYNC"].ToString();
                            dgvr.Cells["STATUS_CODE"].Value = dr["STATUS_CODE"].ToString();
                            dataGridView1.Rows[i].ReadOnly = true;
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                editing(dataGridView1, 1);
                dataGridView1.Columns[8].ReadOnly = false;
                try
                {
                    //foreach (DataGridViewRow row in dataGridView1.Rows)
                    //{
                    //    row.DefaultCellStyle.BackColor = Color.White;
                    //}
                    dataGridView2.Rows.Clear();
                    dataGridView3.Rows.Clear();
                    dataGridView4.Rows.Clear();
                    int a = dataGridView1.CurrentRow.Index;
                    //dataGridView1.Rows[a].DefaultCellStyle.BackColor = Color.SkyBlue;
                    string h = dataGridView1.Rows[a].Cells["UNIQUE_KEY"].Value.ToString();
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
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
                if (dataGridView1.Rows.Count > 0)  // Grid 1
                {
                    editing(dataGridView1, 0);
                   // dataGridView1.Columns[0].ReadOnly = true;
                    //dataGridView1.Columns[1].ReadOnly = true;
                    dataGridView1.Columns[2].ReadOnly = true;
                    dataGridView1.Columns[3].ReadOnly = true;
                    //dataGridView1.Columns[4].ReadOnly = false;
                    //dataGridView1.Columns[5].ReadOnly = true;
                   // dataGridView1.Columns[6].ReadOnly = true;
                    //dataGridView1.Columns[7].ReadOnly = true;
                    //dataGridView1.Columns[8].ReadOnly = false;
                    //dataGridView1.Columns[9].ReadOnly = true;
                    //dataGridView1.Columns[10].ReadOnly = true;
                    //dataGridView1.Columns[11].ReadOnly = true;
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
                    //dataGridView2.Columns[0].ReadOnly = true;
                    //dataGridView2.Columns[1].ReadOnly = true;
                    //dataGridView2.Columns[2].ReadOnly = true;
                    //dataGridView2.Columns[7].ReadOnly = true;
                    //dataGridView2.Columns[8].ReadOnly = true;
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

        //private void dgv1editing(DataGridView dg, int bjzt)
        //{
        //    if (bjzt == 0)
        //    {
        //        int a = dataGridView1.CurrentRow.Index;

        //        dg.Rows[a].ReadOnly = false; //Enable Edit

        //    }
        //    else
        //    {
        //        for (int i = 0; i < dg.Rows.Count; i++)
        //        {
        //            dg.Rows[i].ReadOnly = true; // Disable Edit
        //        }
        //    }
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0 || dataGridView3.Rows.Count > 0 || dataGridView4.Rows.Count > 0)
            {
                int a = dataGridView1.CurrentRow.Index;
                if (dataGridView1.Rows.Count > 0 || dataGridView1.Rows[a].Cells["UNIQUE_KEY"].Value.ToString() == dataGridView2.Rows[0].Cells["UNION_ID"].Value.ToString())
                {
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();
                    DataTable dt4 = new DataTable();

                    //foreach (DataGridViewRow row in dataGridView1.Rows)
                    //    bool isSelected = Convert.ToBoolean(row.Cells["checkBoxColumn"].Value);
                    //if (isSelected)

                    //datagridview1 to dt1
                    if (dataGridView1.Rows.Count > 0)
                    {
                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                            dt1.Columns.Add(column.Name);


                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            bool isSelected = Convert.ToBoolean(row.Cells["chk"].Value);
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



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "delete")
                {
                    DialogResult dr = MessageBox.Show("Are you sure you want to delete!", "Prompt", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dr == DialogResult.OK)
                    {
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        string UNIQUE_KEY = dataGridView1.Rows[e.RowIndex].Cells["UNIQUE_KEY"].Value.ToString();
                        p.Add("UNIQUE_KEY", UNIQUE_KEY);
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
                     
                    dataGridView1.Rows[e.RowIndex].Cells["chk"].Value = "true";

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Index != e.RowIndex)
                        {
                            dataGridView1.Rows[row.Index].Cells["chk"].Value = "false";
                        }
                    }
                    //dataGridView1.Rows[e.RowIndex].Cells["chk"].Value = "true";
                }
                
            }
        }

        //private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex > 0)
        //    {
        //        if ((bool)(this.dataGridView2.Rows[e.RowIndex].Cells[0].Value == null ? false : this.dataGridView2.Rows[e.RowIndex].Cells[0].Value))
        //        {
        //            for (int i = 1; i < dataGridView2.ColumnCount; i++)
        //            {
        //                this.dataGridView2.Rows[e.RowIndex].Cells[i].ReadOnly = false;
        //            }
        //        }
        //        else
        //        {
        //            for (int i = 1; i < dataGridView2.ColumnCount; i++)
        //            {
        //                this.dataGridView2.Rows[e.RowIndex].Cells[i].ReadOnly = true;
        //            }
        //        }
        //    }

        //}


        //private void dgv1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
        //    {
        //        //do stuff
        //    }
        //}



    }//Class
}
