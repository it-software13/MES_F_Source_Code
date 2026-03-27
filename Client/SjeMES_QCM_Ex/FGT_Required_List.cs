using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SjeMES_QCM_Ex
{
    public partial class FGT_Required_List : MaterialForm
    {
        public FGT_Required_List()
        {
            InitializeComponent();
            dataGridView1.CurrentCellDirtyStateChanged += DataGridView1_CurrentCellDirtyStateChanged;
        }
        private void FGT_Required_List_Load(object sender, EventArgs e)
        {
            cb_fgt_result.Items.Insert(0, "");
        }

        private void Btn_search_Click(object sender, EventArgs e)
        {
            MonthlyFGTReport(dt_Month.Text, cb_fgt_result.Text);
        }

        public void MonthlyFGTReport(string Month,string FGT_Result)
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("Month", Month);
                p.Add("FGT_Result", FGT_Result);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_MESAPI",
                                            "SJ_MESAPI.FGT_Digitalization",
                                            "MonthlyFGTReport",
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                    dataGridView1.DataSource = null;
                    //dataGridView1.Rows.Clear();
                    if (dt.Rows.Count > 0)
                    {
                        AutogenerateColumns(dt);
                        dataGridView1.DataSource = dt;
                        ((DataTable)dataGridView1.DataSource).AcceptChanges();
                    }
                }
                
                else
                {
                    dataGridView1.DataSource = null;
                    //dataGridView1.Rows.Clear();
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
               
            }
            catch (Exception ex)
            {
                dataGridView1.DataSource = null;
                //dataGridView1.Rows.Clear();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }


        public void AutogenerateColumns(DataTable dt)
        {

            var sizes = dt.AsEnumerable()
              .Select(r => r["SHOE_SIZE"].ToString())
              .Distinct()
              .ToList();

           
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "PROD_NO",
                HeaderText = "PROD_NO",
                DataPropertyName = "PROD_NO"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "MODEL_NAME",
                HeaderText = "MODEL_NAME",
                DataPropertyName = "MODEL_NAME"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "PO_NUMBER",
                HeaderText = "PO_NUMBER",
                DataPropertyName = "PO_NUMBER"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "WORK_CENTER",
                HeaderText = "WORK_CENTER",
                DataPropertyName = "WORK_CENTER"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TEST_RESULT",
                HeaderText = "TEST_RESULT",
                DataPropertyName = "TEST_RESULT"
            });

            DataGridViewComboBoxColumn comboCol1 = new DataGridViewComboBoxColumn();
            comboCol1.Name = "SHOE_SIZE";
            comboCol1.HeaderText = "SHOE_SIZE";
            comboCol1.DataPropertyName = "SHOE_SIZE";

            // Keep empty initially
            comboCol1.DataSource = sizes;
            dataGridView1.Columns.Add(comboCol1);


            DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
            comboCol.Name = "TEST_TYPE";
            comboCol.HeaderText = "TEST_TYPE";
            comboCol.DataPropertyName = "TEST_TYPE";

            // Dropdown values
            comboCol.Items.Add("Half_Test");
            comboCol.Items.Add("Full_Test");

            dataGridView1.Columns.Add(comboCol);

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "QUANTITY",
                HeaderText = "QUANTITY",
                DataPropertyName = "QUANTITY",
                ReadOnly = true
            });

            dataGridView1.Columns.Add(new DataGridViewDisableButtonColumn()
            {
                Name = "CONFIRM",
                HeaderText = "CONFIRM",
                Text = "CONFIRM",
                UseColumnTextForButtonValue = true
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "DATE_START_PLAN",
                HeaderText = "DATE_START_PLAN",
                DataPropertyName = "DATE_START_PLAN"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "CR_REQDATE",
                HeaderText = "CR_REQDATE",
                DataPropertyName = "CR_REQDATE"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "LAB_REQUESTED_DATE",
                HeaderText = "LAB_REQUESTED_DATE",
                DataPropertyName = "LAB_REQUESTED_DATE"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "PROD_SEND_DATE",
                HeaderText = "PROD_SEND_DATE",
                DataPropertyName = "PROD_SEND_DATE"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "LAB_CONFIRMED_DATE",
                HeaderText = "LAB_CONFIRMED_DATE",
                DataPropertyName = "LAB_CONFIRMED_DATE"
            });

        }
        

        private void Btn_save_Click(object sender, EventArgs e)
        {
            SaveFGT_RequiredQty();
        }


        public void SaveFGT_RequiredQty()
        {

            DataTable dt = (DataTable)dataGridView1.DataSource;

            var filteredRows = dt.AsEnumerable()
    .Where(row =>
        row.RowState == DataRowState.Added ||
        (
            row.RowState == DataRowState.Modified &&
            (
                !Equals(row["TEST_TYPE", DataRowVersion.Original], row["TEST_TYPE", DataRowVersion.Current]) &&
                !Equals(row["SHOE_SIZE", DataRowVersion.Original], row["SHOE_SIZE", DataRowVersion.Current])
            )
        )
    );

    DataTable FGT_Articles = filteredRows.Any()
    ? filteredRows.CopyToDataTable()
    : dt.Clone();

           if(FGT_Articles!=null && FGT_Articles.Rows.Count > 0)
            {
                try
                {
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("FGT_Articles", FGT_Articles);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJ_MESAPI",
                                                "SJ_MESAPI.FGT_Digitalization",
                                                "SaveFGT_RequiredQty",
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (ret.IsSuccess)
                    {
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                        MonthlyFGTReport(dt_Month.Text, cb_fgt_result.Text);

                        //Send Whatsapp alert after saving data.
                        foreach (DataRow dr in FGT_Articles.Rows)
                        {
                            string Msg = ConvertFGTRequestToWhatsApp(dr);
                            SendFGTRequestDetails(Msg);

                        }
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                        MonthlyFGTReport(dt_Month.Text, cb_fgt_result.Text);
                    }


                }
                catch (Exception ex)
                {
                    dataGridView1.Rows.Clear();
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }

                
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select Both Shoe Size and Test Type");
            }
           

        }

        public static string ConvertFGTRequestToWhatsApp(DataRow dr)
        {
            if (dr == null)
                return "No data available.";

            string prodNo = dr["PROD_NO"]?.ToString();
            string Model_Name = dr["MODEL_NAME"]?.ToString();
            string PO_Number = dr["PO_NUMBER"]?.ToString();
            string workCenter = dr["WORK_CENTER"]?.ToString();
            string ShoeSize = dr["SHOE_SIZE"]?.ToString();
            string startDate = Convert.ToDateTime(dr["DATE_START_PLAN"]).ToString("dd-MMM-yyyy");
            string crd = Convert.ToDateTime(dr["CR_REQDATE"]).ToString("dd-MMM-yyyy");
            string testType = dr["TEST_TYPE"]?.ToString();
            string quantity = dr["QUANTITY"]?.ToString();

            var sb = new StringBuilder();

            sb.AppendLine("👟 *FGT Lab Test Request*");
            sb.AppendLine("Dear Production Team,");
            sb.AppendLine("");
            sb.AppendLine("Kindly arrange and send the below shoes for lab testing:");
            sb.AppendLine("--------------------------------");

            sb.AppendLine($"🆔 Article       : {prodNo}");
            sb.AppendLine($"👟 Model_Name    : { Model_Name}");
            sb.AppendLine($"📄 PO_Number     : {PO_Number}");
            sb.AppendLine($"🏭 Work Center   : *{workCenter}*");
            sb.AppendLine($"👟 Shoe Size     : { ShoeSize}");
            sb.AppendLine($"📅 Start Date    : {startDate}");
            sb.AppendLine($"📅 CRD           : {crd}");
            sb.AppendLine($"🧪 Test Type     : *{testType}*");
            sb.AppendLine($"📦 Required Qty  : *{quantity}*");

            sb.AppendLine("");
            sb.AppendLine("⚠️ Please prioritize and dispatch at the earliest.");
            sb.AppendLine("🙏 Thanks & Regards");
            sb.AppendLine("QIP Lab Team");

            return sb.ToString();
        }


        public async Task SendFGTRequestDetails(string msg)
        {
            string apiUrl = "http://10.3.0.208:9090/whatsapp/WhatsappApi/SendMessage";

            var payload = new
            {
                numbers = new List<string>(), // Use the fetched phone number
                groups = new[] { "120363407101751518@g.us" },//120363423613523406@g.us(AQL Inspection Result)//120363347683285873@g.us(test)
                textMsg = msg,
                mediaurl = "",
                filename = ""
            };

            //var payload = new
            //{
            //    numbers = new[] { "9640416084" }, // Use the fetched phone number
            //    groups = new List<string>(),
            //    textMsg = msg,
            //    mediaurl = "",
            //    filename = ""
            //};



            var jsonPayload = JsonConvert.SerializeObject(payload);

            using (var httpClient = new HttpClient())
            {
                try
                {
                    // Set the content type to application/json
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    // Send the POST request
                    var response = await httpClient.PostAsync(apiUrl, content); // Ensure url is defined

                    // Optionally log the response or handle errors here, but do not return
                    if (!response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        // Log the failure if needed
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (logging, etc.) but do not return
                }
            }
        }

        

        private void DataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }



        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                // --- Row ReadOnly Logic based on TEST_RESULT ---
                var testResult = row.Cells["TEST_RESULT"]?.Value;

                bool hasTestResult = testResult != null &&
                                     testResult != DBNull.Value &&
                                     !string.IsNullOrWhiteSpace(testResult.ToString());

                row.ReadOnly = hasTestResult;

                // --- Button Enable/Disable Logic ---
                var prodSendDate = row.Cells["PROD_SEND_DATE"]?.Value;
                var labConfirmedDate = row.Cells["LAB_CONFIRMED_DATE"]?.Value;

                bool hasProdSendDate = prodSendDate != null &&
                                       prodSendDate != DBNull.Value &&
                                       !string.IsNullOrWhiteSpace(prodSendDate.ToString());

                bool hasLabConfirmedDate = labConfirmedDate != null &&
                                           labConfirmedDate != DBNull.Value &&
                                           !string.IsNullOrWhiteSpace(labConfirmedDate.ToString());

                bool isActive = hasProdSendDate && !hasLabConfirmedDate;

               

                var btnCell = row.Cells["CONFIRM"];
                if (isActive)
                {
                    ((DataGridViewDisableButtonCell)btnCell).Enabled = isActive;
                }
                else
                {
                    ((DataGridViewDisableButtonCell)btnCell).Enabled = isActive;
                }
            }
        }


        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "TEST_TYPE")
            {
                var row = dataGridView1.Rows[e.RowIndex];

                if (row != null && !row.IsNewRow)
                {
                    var value = row.Cells["TEST_TYPE"].Value?.ToString();

                    if (value == "Full_Test")
                    {
                        row.Cells["QUANTITY"].Value = 9;
                    }
                    else if (value == "Half_Test")
                    {
                        row.Cells["QUANTITY"].Value = 4;
                    }
                }
            }
        }
       

       private DataTable GetSizes(string PO_Number,string WorkCenter)
        {
                DataTable dt = new DataTable();
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("PO_Number", PO_Number);
                p.Add("WorkCenter", WorkCenter);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_MESAPI",
                                            "SJ_MESAPI.FGT_Digitalization",
                                            "GetShoeSize",
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);

            }
            return dt;
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            var row = dataGridView1.Rows[e.RowIndex];

            if (columnName == "SHOE_SIZE")
            {
                string PO_Number = row.Cells["PO_NUMBER"].Value?.ToString();
                string workCenter = row.Cells["WORK_CENTER"].Value?.ToString();

                DataTable dt = GetSizes(PO_Number, workCenter);

                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)row.Cells["SHOE_SIZE"];

                cell.DataSource = null;
                cell.DataSource = dt;
                cell.DisplayMember = "SIZE_NO";
                cell.ValueMember = "SIZE_NO";
            }

            else if (columnName == "TEST_TYPE")
            {
                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)row.Cells["TEST_TYPE"];

                cell.DataSource = null;
                cell.Items.Clear();

                cell.Items.Add("Half_Test");
                cell.Items.Add("Full_Test");
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "CONFIRM")
            {
                var row = dataGridView1.Rows[e.RowIndex];

                var btnCell = row.Cells["CONFIRM"] as DataGridViewDisableButtonCell;

                if (btnCell == null)
                    return; // safety check

                bool isActive = btnCell.Enabled;

                if (!isActive)
                    return; // 🚫 disabled button

                string Prod_No = row.Cells["PROD_NO"]?.Value?.ToString();
                string CRD = row.Cells["CR_REQDATE"]?.Value?.ToString();
                SaveLabConfirmation(Prod_No, CRD);

            }
        }

        public void SaveLabConfirmation(string Prod_No, string CRD)
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("Prod_No", Prod_No);
                p.Add("CRD", CRD);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_MESAPI",
                                            "SJ_MESAPI.FGT_Digitalization",
                                            "SaveLabConfirmation",
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                    MonthlyFGTReport(dt_Month.Text, cb_fgt_result.Text);
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    MonthlyFGTReport(dt_Month.Text, cb_fgt_result.Text);
                }


            }
            catch (Exception ex)
            {
                dataGridView1.Rows.Clear();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        public class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
        {
            public DataGridViewDisableButtonColumn()
            {
                this.CellTemplate = new DataGridViewDisableButtonCell();
            }
        }

        public class DataGridViewDisableButtonCell : DataGridViewButtonCell
        {
            private bool enabledValue;
            public bool Enabled
            {
                get
                {
                    return enabledValue;
                }
                set
                {
                    enabledValue = value;
                }
            }

            public override object Clone()
            {
                DataGridViewDisableButtonCell cell =
                (DataGridViewDisableButtonCell)base.Clone();
                cell.Enabled = this.Enabled;
                return cell;
            }

            public DataGridViewDisableButtonCell()
            {
                this.enabledValue = true;
            }

            protected override void Paint(Graphics graphics,
            Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates elementState, object value,
            object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
            {
                if (!this.enabledValue)
                {
                    if ((paintParts & DataGridViewPaintParts.Background) ==
                    DataGridViewPaintParts.Background)
                    {
                        SolidBrush cellBackground =
                        new SolidBrush(cellStyle.BackColor);
                        graphics.FillRectangle(cellBackground, cellBounds);
                        cellBackground.Dispose();
                    }

                    if ((paintParts & DataGridViewPaintParts.Border) ==
                    DataGridViewPaintParts.Border)
                    {
                        PaintBorder(graphics, clipBounds, cellBounds, cellStyle,
                        advancedBorderStyle);
                    }
                    Rectangle buttonArea = cellBounds;
                    Rectangle buttonAdjustment =
                    this.BorderWidths(advancedBorderStyle);
                    buttonArea.X += buttonAdjustment.X;
                    buttonArea.Y += buttonAdjustment.Y;
                    buttonArea.Height -= buttonAdjustment.Height;
                    buttonArea.Width -= buttonAdjustment.Width;
                    ButtonRenderer.DrawButton(graphics, buttonArea,
                    System.Windows.Forms.VisualStyles.PushButtonState.Disabled);

                    if (this.FormattedValue is String)
                    {
                        TextRenderer.DrawText(graphics,
                        (string)this.FormattedValue,
                        this.DataGridView.Font,
                        buttonArea, SystemColors.GrayText);
                    }
                }
                else
                {
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    elementState, value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle, paintParts);
                }
            }
        }
    }
}
