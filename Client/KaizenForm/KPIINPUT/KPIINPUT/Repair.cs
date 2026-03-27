using AutocompleteMenuNS;
using MaterialSkin.Controls;
using Newtonsoft.Json;
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

namespace KPIINPUT
{
    public partial class Repair : MaterialForm
    {
        
        public Repair()
        {
            InitializeComponent();
            
            this.WindowState = FormWindowState.Maximized;
           //// tabControl1.Dock = DockStyle.Fill;

           // //tabControl1.Dock = DockStyle.Fill;
           // tabControl1.Width = this.ClientSize.Width;
           // tabControl1.Height = this.ClientSize.Height;

           // // Resize Panel inside the TabPage
           // panel1.Width = tabControl1.Width - 5; // Adjust width
           // panel1.Height = tabControl1.Height - 5; // Adjust height

           // // Resize DataGridView inside the Panel
           // dataGridView1.Width = panel1.Width -5;
           // dataGridView1.Height = panel1.Height - 5;




        }
        public void clear()
        {
            PRODLINE.Text = "";
            TOTALRECEIVED.Text = "";
            TOTALREPAIRED.Text = "";
            REMAININGQTY.Text = "";
            REPAIRREASON.SelectedIndex = -1;
            txtRepairReason.Visible = false;
            label5.Visible = false;



        }
        public void LoadQueryItem()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("REPAIRNAME", REPAIRREASON.Text);
           
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                 "KZ_PRODKPIAPI.Controllers.RepairsDataserver",
                 "SelectRepairData", Program.client.UserToken, JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    
                    REASON.Items.Add(dtJson.Rows[i]["REPAIRNAME"].ToString());
                    REPAIRREASON.Items.Add(dtJson.Rows[i]["REPAIRNAME"].ToString());


                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }



        }
       

        private void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PRODLINE.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Production Line");
                return;
            }
            if (string.IsNullOrEmpty(TOTALRECEIVED.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Total Received");
                return;
            }
            if (string.IsNullOrEmpty(TOTALREPAIRED.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Total Repaired");
                return;
            }
            if (string.IsNullOrEmpty(REMAININGQTY.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Total Remaining");
                return;
            }
            if (string.IsNullOrEmpty(REPAIRREASON.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Repair Type");
                return;
            }
            if (string.IsNullOrEmpty(REPAIRDATE.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Repair Date");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;

            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("PRODLINE", PRODLINE.Text);
            retData.Add("TOTALRECEIVED", TOTALRECEIVED.Text);
            retData.Add("TOTALREPAIRED", TOTALREPAIRED.Text);
            retData.Add("REMAININGQTY", REMAININGQTY.Text);
            if (REPAIRREASON.Text.Trim().ToLower() != "others")
            {
                retData.Add("REPAIRREASON", REPAIRREASON.Text);
            }
            else
            {
                if (REPAIRREASON.Text.Trim().ToLower() == "others")
                {
                    if (txtRepairReason == null) // Only create the TextBox if it doesn't already exist
                    {
                        txtRepairReason = new TextBox();
                        txtRepairReason.Location = new Point(507, 75); // Adjust as needed
                        txtRepairReason.Size = new Size(264, 50);     // Adjust as needed
                                                                      //this.Controls.Add(txtRepairReason)
                        retData.Add("REPAIRREASON", txtRepairReason.Text);
                    }

                    // Make it visible and clear any previous text
                    label5.Visible = true;
                    txtRepairReason.Visible = true;

                    retData.Add("REPAIRREASON", txtRepairReason.Text);
                    if (string.IsNullOrEmpty(txtRepairReason.Text))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Other Repair Reason");
                        return;

                    }


                }
                else
                {
                    if (txtRepairReason != null)
                    {
                        txtRepairReason.Visible = false;  // Simply hide it instead of removing it
                        label5.Visible = false;
                    }
                }

            }

            retData.Add("REPAIRDATE", REPAIRDATE.Value.ToString("yyyy-MM-dd"));
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                 "KZ_PRODKPIAPI.Controllers.RepairsDataserver",
                 "SendBGradeRepairData",
Program.client.UserToken,
Newtonsoft.Json.JsonConvert.SerializeObject(retData)
);

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    dataGridView1.DataSource = dtJson;

                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["ErrMsg"].ToString());
            }
            clear();
            LoadTodayInputData();
        }






        private void PRODLINE_TextChanged(object sender, EventArgs e)
        {

        }

        private void TOTALRECEIVED_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TOTALRECEIVED_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TOTALREPAIRED_KeyPress(object sender, KeyPressEventArgs e)
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

        private void REMAININGQTY_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Repair_Load(object sender, EventArgs e)
        {
            LoadQueryItem();
            LoadTodayInputData();
            LoadProd_Line();

        }

        private void LoadTodayInputData()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
           // retData.Add("FROMDATE", DateTime.Now.ToString("yyyy-MM-dd"));
            retData.Add("REPAIRDATE", DateTime.Now.Date.ToString("yyyy-MM-dd"));


            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                 "KZ_PRODKPIAPI.Controllers.RepairsDataserver",
                 "LoadTodayData",
Program.client.UserToken,
Newtonsoft.Json.JsonConvert.SerializeObject(retData)
);

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    dataGridView1.DataSource = dtJson;
                    


                }

            }
        }

        public void LoadProd_Line()
        {
            PRODLINE.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            PRODLINE.AutoCompleteSource = AutoCompleteSource.CustomSource;
            LINE.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            LINE.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> p = new Dictionary<string, string>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                 "KZ_PRODKPIAPI.Controllers.RepairsDataserver", "GetMESDepts", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    autocompleteMenu1.MaximumSize = new Size(250, 350);
                    var columnWidth = new[] { 50, 200 };

                    int n = 1;
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dtJson.Rows[i]["DEPARTMENT_CODE"].ToString() }, dtJson.Rows[i]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                        n++;
                    }
                }
            }






        }





        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel1_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void REPAIRDATE_ValueChanged(object sender, EventArgs e)
        {
            //DateTimePicker REPAIRDATE = (DateTimePicker)sender;

            //// Set the min and max date to today
            //REPAIRDATE.MinDate = DateTime.Today;
            //REPAIRDATE.MaxDate = DateTime.Today;

            //// If the user changes the date, reset it to today's date
            //if (REPAIRDATE.Value.Date != DateTime.Today)
            //{
            //    REPAIRDATE.Value = DateTime.Today;
            //}

            //// Optionally, update your dictionary with today's date
            //Dictionary<string, object> retData = new Dictionary<string, object>();
            //retData.Add("REPAIRDATE", DateTime.Today.ToString("yyyy-MM-dd"));
        }
        
        private void REPAIRREASON_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            if (REPAIRREASON.Text.Trim().ToLower() == "others")
            {
                if (txtRepairReason == null) // Only create the TextBox if it doesn't already exist
                {
                    txtRepairReason = new TextBox();
                    txtRepairReason.Location = new Point(507, 75); // Adjust as needed
                    txtRepairReason.Size = new Size(264, 50);     // Adjust as needed
                                                                  //this.Controls.Add(txtRepairReason)
                    retData.Add("REPAIRREASON", txtRepairReason.Text);
                }

                // Make it visible and clear any previous text
                txtRepairReason.Visible = true;
                label5.Visible = true;
                txtRepairReason.Text = "";
                retData.Add("REPAIRREASON", txtRepairReason.Text);


            }
            else
            {
                if (txtRepairReason != null)
                {
                    txtRepairReason.Visible = false;  // Simply hide it instead of removing it
                    label5.Visible = false;
                }
            }

        }

        private void Label5_Click(object sender, EventArgs e)
        {
            
        }

        private void Label10_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("FROMDATE", FROMDATE.Value.ToString("yyyy-MM-dd"));
            retData.Add("TODATE", TODATE.Value.ToString("yyyy-MM-dd"));
            retData.Add("PRODLINE", LINE.Text);

            retData.Add("REPAIRREASON", REASON.Text);

            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                 "KZ_PRODKPIAPI.Controllers.RepairsDataserver",
                 "ViewRepairData",
Program.client.UserToken,
Newtonsoft.Json.JsonConvert.SerializeObject(retData)
);

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    dataGridView2.DataSource = dtJson;
                    REASON.SelectedIndex = -1;
                    //LINE.Text = "";
                }

            }
            else
            {
                dataGridView2.IsEmpty();
                dataGridView2.DataSource = null;
                REASON.SelectedIndex = -1;
                //LINE.Text = "";
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");

            }
        }

        private void REMAININGQTY_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void TOTALREPAIRED_TextChanged(object sender, EventArgs e)
        {
            

                double value11 = double.TryParse(TOTALRECEIVED.Text, out var result11) ? result11 : 0;
                double value12 = double.TryParse(TOTALREPAIRED.Text, out var result12) ? result12 : 0;
                REMAININGQTY.Text = (value11 - value12).ToString();
            
            
            if(value12>value11)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Total Repaired Is More Than The Total Received ");
                if (TOTALREPAIRED.Text.Length > 0)
                {
                    TOTALREPAIRED.Text = TOTALREPAIRED.Text.Substring(0, TOTALREPAIRED.Text.Length - 1);
                    TOTALREPAIRED.SelectionStart = TOTALREPAIRED.Text.Length;

                }
                
                return;
            }




        }

        private void REMAININGQTY_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void LINE_TextChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            LoadTodayInputData();

        }

        private void FROMDATE_ValueChanged(object sender, EventArgs e)
        {

        }

        private void TabPage1_Click(object sender, EventArgs e)
        {

        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void TxtRepairReason_TextChanged(object sender, EventArgs e)
        {

        }

        private void REASON_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TOTALRECEIVED_TextChanged(object sender, EventArgs e)
        {
            if (TOTALRECEIVED.Text=="")
            {
                REMAININGQTY.Text = "";
            }
        }
    }
}
    

    

