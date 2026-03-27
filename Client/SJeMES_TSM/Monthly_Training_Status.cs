using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using SJeMES_Framework.WebAPI;

namespace SJeMES_TSM
{
    public partial class Monthly_Training_Status : MaterialForm
    {
        public Monthly_Training_Status()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string fromDate = dateTimePicker1.Text;
                string toDate = dateTimePicker2.Text;
                string Training_Type = comboBox1.Text;
                string Process_Type = comboBox2.Text;
                if (string.IsNullOrEmpty(fromDate))
                {
                    MessageBox.Show("Select from Date");
                    return;
                }
                if (string.IsNullOrEmpty(toDate))
                {
                    MessageBox.Show("Select To Date");
                    return;
                }
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("fromDate", fromDate);
                p.Add("toDate", toDate);
                p.Add("Training_Type", Training_Type);
                p.Add("Process_Type", Process_Type);
                string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                      Program.Client.APIURL,
                      "SJ_TSMAPI",
                      "SJ_TSMAPI.Training_Status",
                      "CountDetails",
                      Program.Client.UserToken,
                      Newtonsoft.Json.JsonConvert.SerializeObject(p)
                  );

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);

                if (ret.IsSuccess)
                {

                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());

                    //comboBox4.Items.Clear();

                    if (dtJson1.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dtJson1;
                        //dataGridView1.ReadOnly = true;
                    }

                    else
                    {
                        dataGridView1.DataSource = null;
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                    }
                }
                else
                {
                    dataGridView1.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }




            }
            catch (Exception ex)
            {

                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
