using MaterialSkin.Controls;
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

namespace Sterilization_Alert
{
    public partial class Email_Alert_Days_Submit : MaterialForm
    {
        public Email_Alert_Days_Submit()
        {
            InitializeComponent();
        }

        private void Email_Alert_Days_Submit_Load(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("Days", comboBox1.Text);

                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                              "SJ_MESAPI", "SJ_MESAPI.Sterilization", "Dayssubmit", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Submitted successfully!");
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, j["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }
}
