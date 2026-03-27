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

namespace SjeMES_QCM_Ex
{
    public partial class FGT_Requested_List : MaterialForm
    {
        public FGT_Requested_List()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GetFGT_Requested_List();
        }
        public void GetFGT_Requested_List()
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("CRD_Month", dateTimePicker1.Text);
                p.Add("Plant", textBox1.Text);
                p.Add("Status", cb_status.Text);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_MESAPI",
                                            "SJ_MESAPI.FGT_Digitalization",
                                            "GetFGT_Requested_List",
                                            Program.Client.UserToken,
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                    AutogenerateColumns();
                    dataGridView1.DataSource = dt;
                    ((DataTable)dataGridView1.DataSource).AcceptChanges();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    dataGridView1.DataSource = null;
                }


            }
            catch (Exception ex)
            {
                dataGridView1.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        public void AutogenerateColumns()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewDisableButtonColumn()
            {
                Name = "SUBMIT",
                HeaderText = "SUBMIT",
                Text = "SUBMIT",
                UseColumnTextForButtonValue = true
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "CR_REQDATE",
                HeaderText = "CR_REQDATE",
                DataPropertyName = "CR_REQDATE"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "PROD_NO",
                HeaderText = "PROD_NO",
                DataPropertyName = "PROD_NO"
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
                Name = "SHOE_SIZE",
                HeaderText = "SHOE_SIZE",
                DataPropertyName = "SHOE_SIZE"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TEST_TYPE",
                HeaderText = "TEST_TYPE",
                DataPropertyName = "TEST_TYPE"
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

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "SUBMIT")
            {
                var row = dataGridView1.Rows[e.RowIndex];

                var btnCell = row.Cells["SUBMIT"] as DataGridViewDisableButtonCell;

                if (btnCell == null)
                    return; // safety check

                bool isActive = btnCell.Enabled;

                if (!isActive)
                    return; // 🚫 disabled button

                string Prod_No = row.Cells["PROD_NO"]?.Value?.ToString();
                string CRD = row.Cells["CR_REQDATE"]?.Value?.ToString();
                SaveProductionSubmit(Prod_No, CRD);
            }
        }

        public void SaveProductionSubmit(string Prod_No, string CRD)
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
                                            "SaveProductionSubmit",
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                    GetFGT_Requested_List();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    GetFGT_Requested_List();
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

        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // --- Button Enable/Disable Logic ---
                var labConfirmedDate = row.Cells["PROD_SEND_DATE"]?.Value;

                bool hasLabConfirmedDate = labConfirmedDate != null &&
                                           labConfirmedDate != DBNull.Value &&
                                           !string.IsNullOrWhiteSpace(labConfirmedDate.ToString());

                bool isActive =  !hasLabConfirmedDate;



                var btnCell = row.Cells["SUBMIT"];
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
    }
}
