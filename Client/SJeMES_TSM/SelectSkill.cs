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
    public partial class SelectSkill : Form
    {
        private FlowLayoutPanel flowLayoutPanel;
        string Barcode;
        string ProdLine;
        public string Result { get; private set; }
        public SelectSkill(string barcode, string prodline)
        {
            Barcode = barcode;
            ProdLine = prodline;
            InitializeComponent();
            flowLayoutPanel = new FlowLayoutPanel
            {
                Location = new Point(10, 10),
                Size = new Size(816, 489),
            };

            this.Controls.Add(flowLayoutPanel);
            LoadDataAndCreateButtons();

            this.ClientSize = new Size(816, 489); // Set form size
            this.StartPosition = FormStartPosition.CenterParent; // Center relative to parent
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Optional: fixed dialog style
            this.MaximizeBox = false; // Disable maximize button
            this.MinimizeBox = false; // Disable minimize button
        }

        private void LoadDataAndCreateButtons()
        {

            DataTable dataTable = GetSkillsList(Barcode);

            foreach (DataRow row in dataTable.Rows)
            {
                string buttonText = row["skill_name"].ToString();
                Button button = new Button
                {
                    Text = buttonText,
                    AutoSize = true,
                    BackColor = Color.Aqua,
                    ForeColor = Color.Black,
                    Font = new Font("Times New Roman", 16, FontStyle.Bold),
                    Size = new Size(150, 50)

                };
                button.Click += (sender, e) =>
                {
                    Result = button.Text;
                    this.Close();
                };
                flowLayoutPanel.Controls.Add(button);
            }

        }
        public DataTable GetSkillsList(string Barcode)
        {
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Barcode", Barcode);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                          Program.Client.APIURL,
                                          "SJ_TSMAPI",
                                          "SJ_TSMAPI.Production_Adjustment",
                                          "GetSkillsList",//方法名
                                          Program.Client.UserToken,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
            return dt;
        }

    }
}
