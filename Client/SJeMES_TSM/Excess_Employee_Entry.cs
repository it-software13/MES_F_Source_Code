using MaterialSkin.Controls;
using SJeMES_Control_Library;
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

namespace SJeMES_TSM
{
    public partial class Excess_Employee_Entry : MaterialForm
    {
        public Excess_Employee_Entry()
        {
            InitializeComponent();
        }

        private void Excess_Employee_Entry_Load(object sender, EventArgs e)
        {
            dt_prod_date.MinDate = DateTime.Now;
            dt_prod_date.MaxDate = DateTime.Now;
            string ProdLine = GetUserLine();
            txt_ProdLine.Text = ProdLine;
            txtprodline2.Text = ProdLine;
            if (!string.IsNullOrEmpty(txt_ProdLine.Text))
            {
                GetLineEmployee(txt_ProdLine.Text, dt_prod_date.Text);
            }
            else
            {
                MessageHelper.ShowErr(this, "No Production Line Assigned for this Account");
            }
        }

        public string GetUserLine()
        {
            string ProdLine = string.Empty;
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                          Program.Client.APIURL,
                                          "SJ_TSMAPI",//类库名
                                          "SJ_TSMAPI.Production_Adjustment",//类名
                                          "GetUserLine",//方法名
                                          Program.Client.UserToken,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            // string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetAllDepts", Program.Client.UserToken, JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(ret.IsSuccess))
            {
                ProdLine = ret.RetData;
            }
            else
            {
                MessageHelper.ShowErr(this, ret.ErrMsg);
            }


            return ProdLine;
        }

        public void GetLineEmployee(string ProdLine, string Prod_Date)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("ProdLine", ProdLine);
            data.Add("Prod_Date", Prod_Date);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                          Program.Client.APIURL,
                                          "SJ_TSMAPI",
                                          "SJ_TSMAPI.Production_Adjustment",
                                          "GetLineEmployee",//方法名
                                          Program.Client.UserToken,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
            if (Convert.ToBoolean(ret.IsSuccess))
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageHelper.ShowErr(this, "No Data Found");
                    dataGridView1.DataSource = null;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, ret.ErrMsg);
            }
        }

        private void Btn_submit_Click(object sender, EventArgs e)
        {

            TimeSpan cutoffTime = new TimeSpan(09, 00, 0); // 09:00 AM

            // Get current time
            TimeSpan currentTime = DateTime.Now.TimeOfDay;

            if (currentTime > cutoffTime)
            {
                MessageBox.Show("You cannot submit data after 09:00 AM.",
                        "Submission Blocked",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                GetLineEmployee(txt_ProdLine.Text, dt_prod_date.Text);
                return; // stop further execution
            }
            else
            {
                Submit_Excess_report();
            }
        }

        public void Submit_Excess_report()
        {
            DataTable dt = new DataTable();
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    dt.Columns.Add(column.Name);


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["Select"].Value);
                    if (isSelected)
                    {
                        DataRow dRow = dt.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dRow[cell.ColumnIndex] = cell.Value;
                        }
                        dt.Rows.Add(dRow);

                    }
                }
            }

            if (dt.Rows.Count > 0)
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("dt", dt);
                data.Add("Prod_Date", dt_prod_date.Text);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                              Program.Client.APIURL,
                                              "SJ_TSMAPI",
                                              "SJ_TSMAPI.Production_Adjustment",
                                              "SaveExcessEmployee",
                                              Program.Client.UserToken,
                                              Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
                if (Convert.ToBoolean(ret.IsSuccess))
                {
                    MessageHelper.ShowSuccess(this, ret.ErrMsg);
                    GetLineEmployee(txt_ProdLine.Text, dt_prod_date.Text);
                }
                else
                {
                    MessageHelper.ShowErr(this, ret.ErrMsg);
                    GetLineEmployee(txt_ProdLine.Text, dt_prod_date.Text);
                }
            }
            else
            {
                MessageHelper.ShowErr(this, "Please select absent employee");
            }
        }

        private void Btn_refresh_Click(object sender, EventArgs e)
        {   
            GetLineEmployee(txt_ProdLine.Text, dt_prod_date.Text);
        }

        private void Btn_search_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("From_Date", datetime_s.Text);
            data.Add("To_Date", datetime_e.Text);
            data.Add("ProdLine", txtprodline2.Text);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                          Program.Client.APIURL,
                                          "SJ_TSMAPI",
                                          "SJ_TSMAPI.Production_Adjustment",
                                          "GetExcessReport",//方法名
                                          Program.Client.UserToken,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
            if (Convert.ToBoolean(ret.IsSuccess))
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    dataGridView2.DataSource = dt;
                }
                else
                {
                    MessageHelper.ShowErr(this, "No Data Found");
                    dataGridView2.DataSource = null;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, ret.ErrMsg);
            }
        }


        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
            {
                // Commit checkbox edit (required)
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);

                // Read the current row values
                bool isChecked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["Select"].Value);
                string barcode = dataGridView1.Rows[e.RowIndex].Cells["emp_no"].Value?.ToString();

                if (isChecked)
                {
                    using (SelectSkill popup = new SelectSkill(barcode, txt_ProdLine.Text))
                    {
                        popup.ShowDialog(this);
                        string result = popup.Result;
                        if (!string.IsNullOrEmpty(result))
                        {
                            dataGridView1.Rows[e.RowIndex].Cells["Working_Skill"].Value = result;
                        }
                        else
                        {
                            dataGridView1.Rows[e.RowIndex].Cells["Select"].Value = false;
                            dataGridView1.EndEdit();
                            dataGridView1.RefreshEdit();
                        }
                    }
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Working_Skill"].Value = "";
                }
            }
        }
    }
}
