using MaterialSkin;
using MaterialSkin.Controls;
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
    public partial class TQC_Task_Edit_Reason : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string task_no = string.Empty;
        string task_state = string.Empty;
        public TQC_Task_Edit_Reason(string _task_no,string _task_state)
        {
            InitializeComponent();
            task_no = _task_no;
            task_state = _task_state;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TQC_Task_Edit_state(task_no,task_state);
        }

        /// <summary>
        /// tqc创建页面设置停线或者重新开线
        /// </summary>
        public void TQC_Task_Edit_state(string task_no, string task_state)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task_no);
                data.Add("task_state", task_state);
                data.Add("reason", richTextBox1.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "TQC_Task_Edit_state", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("conversion successful!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    this.Close();
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }
}
