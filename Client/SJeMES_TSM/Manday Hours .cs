using NewExportExcels;
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
using MaterialSkin.Controls;
namespace SJeMES_TSM
{
    public partial class Manday_Hours : MaterialForm
    {
        public Manday_Hours()
        {
            InitializeComponent();
        }


        private void SearchBtn_Click(object sender, EventArgs e)
        {
            try

            {
                Cursor.Current = Cursors.WaitCursor;
                string fromDate = dateTimePicker1.Text;
                string toDate = dateTimePicker2.Text;
                string Process_Type = comboBox1.Text;
                string Skill_type = comboBox2.Text;
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
                //dataGridView1.DataSource = null;
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("fromDate", fromDate);
                p.Add("toDate", toDate);
                p.Add("Process_Type", Process_Type);
                p.Add("Skill_type", Skill_type);

                string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                       Program.Client.APIURL,
                       "SJ_TSMAPI",
                       "SJ_TSMAPI.ManDayHours",
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

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "ManDay_Hours_Data.xls";
                ExportExcels.Export(a, dataGridView1);
               // SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }

        }


    }
}

