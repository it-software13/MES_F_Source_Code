using Newtonsoft.Json;
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
    public partial class TQC_Bgrade_Reason : Form
    {
        public int ResultValue { get; private set; } = 0;
        private string Po;
        private string Art;
        private string ProdLine;
        private string Task_No;
        public class BGradeReasonData
        {
            public List<Dictionary<string, object>> Data { get; set; }
        }

        public TQC_Bgrade_Reason(TQC_Task_Edit tQC_Task_Edit, string textBox10, string textBox3,string textBox9, string task_no)
        {
            InitializeComponent();
             Po = textBox3;
             Art = textBox10;
             ProdLine = textBox9;
             Task_No = task_no;
        }

       

        private void Confirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Bgrade_Reason.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Bgrade Reason");
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Po", Po);
                data.Add("Art", Art);
                data.Add("Prodline", ProdLine);
                data.Add("Task_No", Task_No);
                data.Add("Bgrade_Reason", Bgrade_Reason.Text);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "TQC_Bgrade_Reason", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    ShowMessageAndCloseAsync();

                }
                else
                {
                    ResultValue = 0;
                    throw new Exception(j["ErrMsg"].ToString());
                    
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }

        }
        private async void ShowMessageAndCloseAsync()
        {
            string msg = SJeMES_Framework.Common.UIHelper.UImsg("Bgrade Reason Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
            ResultValue = 1;

            await Task.Delay(1000); // Wait for 5 seconds

            this.Close();
        }
        private void Cancel_Click(object sender, EventArgs e)
        {

            ResultValue = 0;
            this.Close();
        }
        public void Bgrade_Reasons()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("BGRADE_REASON", Bgrade_Reason.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "Bgrade_Reasons", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);

            if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
            {
                var reasonData = JsonConvert.DeserializeObject<BGradeReasonData>(result.RetData);

                if (reasonData != null && reasonData.Data != null && reasonData.Data.Count > 0)
                {
                    foreach (var row in reasonData.Data)
                    {
                        if (row.ContainsKey("BGRADE_REASON"))
                        {
                            Bgrade_Reason.Items.Add(row["BGRADE_REASON"].ToString());
                        }
                    }
                }
                else
                {
                    Bgrade_Reason.Text = "No Data Found";
                }
            }




        }
        private void TQC_Bgrade_Reason_Load(object sender, EventArgs e)
        {
            Bgrade_Reasons();

        }
    }
}
