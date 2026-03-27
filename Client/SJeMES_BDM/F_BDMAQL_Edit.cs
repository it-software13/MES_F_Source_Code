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
using Sunny.UI;
using System.Reflection;

namespace SJeMES_BDM
{
    public partial class F_BDMAQL_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDMAQL_Edit()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
          Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_BDMAQL_Edit_Load(object sender, EventArgs e)
        {
            GetDgv();
            GetTitle();
            GetData();
         
        }


        public void GetTitle()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("DOC_TYPE", "AQL_ENUM");
                data.Add("LEVEL_TYPE", "AQL_ENUM_RAW");
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.AQLStandard", "ENUM", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    cbo_type.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                    cbo_type.DisplayMember = "enum_value";
                    cbo_type.ValueMember = "enum_code";
                    cbo_type.SelectedIndex = 0;

                    cbo2.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData1.ToString());
                    cbo2.DisplayMember = "enum_value";
                    cbo2.ValueMember = "enum_code";
                    cbo2.SelectedIndex = 0;
                }


            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.ToString());
            }
        }

        public void GetDgv()
        {
            //string headkey = "id,起始批量,截止批量,样本量字码,样本量,AC 0.010,AC 0.015,AC 0.025,AC 0.040,AC 0.065,AC 0.10,AC 0.15,AC 0.25,AC 0.40,AC 0.65,AC 1.0,AC 1.5,AC 2.5,AC 4.0,AC 6.5,AC 10,AC 15,AC 25,AC 40,AC 65,AC 100,AC 150,AC 250,AC 400,AC 650,AC 1000,AC1 0.010,AC1 0.015,AC1 0.025,AC1 0.040,AC1 0.065,AC1 0.10,AC1 0.15,AC1 0.25,AC1 0.40,AC1 0.65,AC1 1.0,AC1 1.5,AC1 2.5,AC1 4.0,AC1 6.5,AC1 10,AC1 15,AC1 25,AC1 40,AC1 65,AC1 100,AC1 150,AC1 250,AC1 400,AC1 650,AC1 1000";
            string headkey = "id,Starting batch, ending batch, sample size character code, sample size,AC 0.010,AC 0.015,AC 0.025,AC 0.040,AC 0.065,AC 0.10,AC 0.15,AC 0.25,AC 0.40,AC 0.65,AC 1.0,AC 1.5,AC 2.5,AC 4.0,AC 6.5,AC 10,AC 15,AC 25,AC 40,AC 65,AC 100,AC 150,AC 250,AC 400,AC 650,AC 1000,AC1 0.010,AC1 0.015,AC1 0.025,AC1 0.040,AC1 0.065,AC1 0.10,AC1 0.15,AC1 0.25,AC1 0.40,AC1 0.65,AC1 1.0,AC1 1.5,AC1 2.5,AC1 4.0,AC1 6.5,AC1 10,AC1 15,AC1 25,AC1 40,AC1 65,AC1 100,AC1 150,AC1 250,AC1 400,AC1 650,AC1 1000";
            string[] key = headkey.Split(',');
            int i = 0;
            foreach (var item in key)
            {
                if (i < 57)
                {
                    i++;
                    rowMergeView1.Columns.Add("clu" + i, item);
                    if (i > 5)
                    {
                        i++;
                        rowMergeView1.Columns.Add("clu" + i, item + "_i");
                    }
                }

            }
            int intdex = rowMergeView1.Rows.Add();
            for (int j = 1; j < rowMergeView1.ColumnCount; j++)
            {
                rowMergeView1.Rows[intdex].Cells[j].Value = rowMergeView1.Columns[j].HeaderText.Replace("_i", "");
            }
            intdex = rowMergeView1.Rows.Add();
            int cod = 0;
            for (int d = 1; d < rowMergeView1.ColumnCount; d++)
            {
                if (d < 6)
                {
                    rowMergeView1.Rows[intdex].Cells[d].Value = rowMergeView1.Columns[d].HeaderText.Replace("_i", "");
                }
                if (d > 5)
                {
                    if (cod == 0)
                    {
                        rowMergeView1.Rows[intdex].Cells[d].Value = "AC,RE";
                        cod = 1;
                    }
                    else if (cod == 1)
                    {
                        rowMergeView1.Rows[intdex].Cells[d].Value = "sample size";
                        cod = 0;
                    }
                }
            }
            rowMergeView1.Columns[1].Visible = false;
        }

        int index = 0;
        public int new_index = -1;
        public void GetData()
        {
            try
            {
                new_index = -1;
                for (int i = rowMergeView1.Rows.Count - 1; i > 1; i--)
                {
                    rowMergeView1.Rows.RemoveAt(i);
                }
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("HORI_TYPE", cbo_type.SelectedValue);
                data.Add("LEVEL_TYPE", cbo2.SelectedValue);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.AQLStandard", "GitAQL", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                    foreach (DataRow item in dt.Rows)
                    {
                        int intdex = rowMergeView1.Rows.Add();
                        int cl = 0;
                        int dl = 0;
                        int cod = 0;
                        decimal sum = 0;
                        for (int i = 0; i < rowMergeView1.ColumnCount; i++)
                        {
                            if (i == 1)
                            {
                                rowMergeView1.Rows[intdex].Cells[i].Value = item["ID"];
                            }
                            else if (i == 2)
                            {
                                rowMergeView1.Rows[intdex].Cells[i].Value = item["START_QTY"];
                            }
                            else if (i == 3)
                            {
                                rowMergeView1.Rows[intdex].Cells[i].Value = item["END_QTY"];
                            }
                            else if (i == 4)
                            {
                                rowMergeView1.Rows[intdex].Cells[i].Value = item["SAMPLE_QTY"];
                            }
                            else if (i == 5)
                            {
                                rowMergeView1.Rows[intdex].Cells[5].Value = item["VALS"];
                            }
                            else if (i > 5)
                            {
                                if (cl <= 26 || dl <= 26)
                                {
                                    if (cod == 0)
                                    {
                                        cl++;
                                        string AC = item[$@"AC{cl.ToString("00")}"].ToString();
                                        if (string.IsNullOrEmpty(AC))
                                        {
                                            AC = "0";
                                        }
                                        rowMergeView1.Rows[intdex].Cells[i].Value = AC + "," + (decimal.Parse(AC) + 1).ToString();
                                        cod = 1;
                                    }
                                    else if (cod == 1)
                                    {
                                        dl++;
                                        string VAL = item[$@"VAL{dl.ToString("00")}"].ToString();
                                        if (string.IsNullOrEmpty(VAL))
                                        {
                                            VAL = "0";
                                        }
                                        sum += decimal.Parse(VAL);
                                        rowMergeView1.Rows[intdex].Cells[i].Value = VAL;
                                        cod = 0;
                                    }
                                }

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.ToString(), Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (new_index == -1)
            {
                int dex = rowMergeView1.Rows.Add();
                new_index = dex;
                DataGridViewRow drw = rowMergeView1.Rows[dex];
                rowMergeView1.ReadOnly = false;
                rowMergeView1.BeginEdit(true);

            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please save the current data first！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }


        private void RowMergeView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 1)
            {
                if (e.ColumnIndex > 6)
                {

                    string text = rowMergeView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    decimal number = 0;
                    decimal.TryParse(text, out number);
                    if (number == 0 && text != "0")
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("AC,Sample size values can only be numbers", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        GetData();
                        return;
                    }
                }
                if (e.ColumnIndex == 2)
                {
                    string text = rowMergeView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    decimal number = 0;
                    decimal.TryParse(text, out number);
                    if (number == 0 && text != "0")
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("The starting batch can only be a number", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        GetData();
                        return;
                    }
                }
                if (e.ColumnIndex == 5)
                {
                    string text = rowMergeView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    decimal number = 0;
                    decimal.TryParse(text, out number);
                    if (number == 0 && text != "0" && text != "")
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Sample size can only be a number", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        GetData();
                        return;
                    }
                    else if (number < 0)
                    {
                        string msgs = SJeMES_Framework.Common.UIHelper.UImsg("Sample size cannot be negative", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msgs);
                        GetData();
                        return;
                    }
                    else if (number > 99999999)
                    {
                        string msgs = SJeMES_Framework.Common.UIHelper.UImsg("The sample size should not be too outrageous", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msgs);
                        GetData();
                        return;
                    }
                }
            }
        }

        private void rowMergeView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 2)
            {
                rowMergeView1.ReadOnly = true;
            }
            else
            {
                rowMergeView1.ReadOnly = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int cl = 0;
                int dl = 0;
                int cod = 0;
                DataTable dt = new DataTable();
                dt.Columns.Add("HORI_TYPE", typeof(string));
                dt.Columns.Add("LEVEL_TYPE", typeof(string));
                for (int i = 0; i < rowMergeView1.ColumnCount; i++)
                {
                    if (i == 1)
                    {
                        dt.Columns.Add("ID", typeof(string));
                    }
                    if (i == 2)
                    {
                        dt.Columns.Add("START_QTY", typeof(string));
                    }
                    else if (i == 3)
                    {
                        dt.Columns.Add("END_QTY", typeof(string));
                    }
                    else if (i == 4)
                    {
                        dt.Columns.Add("SAMPLE_QTY", typeof(string));
                    }
                    else if (i == 5)
                    {
                        dt.Columns.Add("VALS", typeof(string));
                    }
                    else if (i > 5)
                    {
                        if (cl <= 26 || dl <= 26)
                        {
                            if (cod == 0)
                            {
                                cl++;
                                dt.Columns.Add("AC" + cl.ToString("00"), typeof(string));
                                cod = 1;
                            }
                            else if (cod == 1)
                            {
                                dl++;
                                dt.Columns.Add("VAL" + dl.ToString("00"), typeof(string));
                                cod = 0;
                            }
                        }
                    }
                }

                for (int i = 2; i < rowMergeView1.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    cl = 0;
                    dl = 0;
                    cod = 0;
                    dr["HORI_TYPE"] = cbo_type.SelectedValue;
                    dr["LEVEL_TYPE"] = cbo2.SelectedValue;
                    string valuse_no = string.Empty;
                    for (int j = 0; j < rowMergeView1.ColumnCount; j++)
                    {
                        string text = string.Empty;
                        if (rowMergeView1.Rows[i].Cells[j].Value != null)
                        {
                            text = rowMergeView1.Rows[i].Cells[j].Value.ToString();
                        }
                        if (j == 1)
                        {
                            dr["id"] = text;
                        }
                        if (j == 2)
                        {
                            if (string.IsNullOrEmpty(text))
                            {
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("The initial batch cannot be empty！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                                return;
                            }
                            dr["START_QTY"] = text;

                        }
                        else if (j == 3)
                        {
                            dr["END_QTY"] = text;
                        }
                        else if (j == 4)
                        {
                            dr["SAMPLE_QTY"] = text;
                        }
                        else if (j == 5)
                        {
                            dr["VALS"] = text;
                            valuse_no = text;
                        }
                        else if (j > 5)
                        {
                            if (cl <= 26 || dl <= 26)
                            {
                                if (cod == 0)
                                {
                                    cl++;
                                    if (string.IsNullOrEmpty(text))
                                    {
                                        text = "0";
                                    }
                                    dr[$@"AC{cl.ToString("00")}"] = text.Split(',')[0];
                                    cod = 1;
                                }
                                else if (cod == 1)
                                {
                                    dl++;
                                    if (string.IsNullOrEmpty(text))
                                    {
                                        text = valuse_no;
                                    }
                                    dr[$@"VAL{cl.ToString("00")}"] = text;
                                    cod = 0;
                                }
                            }
                        }

                    }
                    dt.Rows.Add(dr);
                }
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Data", dt);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "SJ_QCMAPI", "SJ_QCMAPI.AQLStandard", "AddAQL", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Data processed successfully！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    GetData();
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    GetData();

                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.ToString(), Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void cbo_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            new_index = -1;
            GetData();
        }

        private void rowMergeView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < 2)
            {
                if (rowMergeView1["DeteleBtn", e.RowIndex].Value.ToString() == "Delete" && rowMergeView1["DeteleBtn", e.RowIndex].Value.ToString() != "Operate")
                {
                    rowMergeView1["DeteleBtn", e.RowIndex] = new DataGridViewTextBoxCell();
                    rowMergeView1["DeteleBtn", e.RowIndex].Value = "Operate";

                }
            }
        }

        private void rowMergeView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (rowMergeView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "删除")
            if (rowMergeView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                if (e.RowIndex == new_index)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please save before deleting！");
                    return;
                }
                string id = rowMergeView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("ID", id);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "SJ_QCMAPI", "SJ_QCMAPI.AQLStandard", "DeleteAQL", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("successfully deleted！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    GetData();
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    GetData();

                }
            }

        }
    }
}
