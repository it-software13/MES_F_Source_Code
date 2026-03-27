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

namespace SJeMES_TQC
{
    public partial class TQC_BGrade_View : Form
    {
        private string Task_No;


        public TQC_BGrade_View(TQC_Task_Edit tQC_Task_Edit, string task_no)
        {
            InitializeComponent();
            Task_No = task_no;
        }

        
        public void Bgrade_View()
        {
            try
            {

                Cursor.Current = Cursors.WaitCursor;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Task_No", Task_No);
                string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "TQC_Bgrade_View", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);


                var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                if (dic.ContainsKey("data") || dic.ContainsKey("Data"))
                {
                    string dataString = dic.ContainsKey("data") ? dic["data"].ToString() : dic["Data"].ToString();
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dataString);

                    if (dtJson1.Rows.Count > 0)
                        dataGridView1.DataSource = dtJson1;
                    else
                    {
                        dataGridView1.DataSource = null;
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                    }
                }
                else
                {
                    dataGridView1.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Key 'data' not found in response.");
                }

            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }
        private void TQC_BGrade_View_Load(object sender, EventArgs e)
        {
            Bgrade_View();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            string rowNumber = (e.RowIndex + 1).ToString();

            using (Font normalFont = new Font("Microsoft YaHei", 9, FontStyle.Regular))
            using (Brush brush = new SolidBrush(grid.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(
                    rowNumber,
                    normalFont,
                    brush,
                    e.RowBounds.Location.X + 15,
                    e.RowBounds.Location.Y + 4
                );
            }
        }
    }
}
