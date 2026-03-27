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

namespace SJeMES_BDM
{
    public partial class F_BDM_Painted_Skin_Insert : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_Painted_Skin_Insert()
        {
            InitializeComponent();
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
            if (Convert.ToDateTime(dateTimePicker1.Text) > Convert.ToDateTime(dateTimePicker2.Text))
            {
                MessageBox.Show("End date cannot be less than start date！");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Data cannot be empty！");
                return;
            }

            InsertPainted_Skin_Insert();
        }

        /// <summary>
        /// 画皮新增
        /// </summary>
        public void InsertPainted_Skin_Insert()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("item_no", this.textBox2.Text.Trim());
                data.Add("wh_date_start", this.dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                data.Add("wh_date_end", this.dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                data.Add("mtl_qty", this.textBox4.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.BDM_Painted_Skin", "InsertPainted_Skin_Insert", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
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

        private void textBox2_Click(object sender, EventArgs e)
        {
            using (F_BDM_Painted_Skin_Insert_item f = new F_BDM_Painted_Skin_Insert_item(this))
            {
                f.ShowDialog();
            }
        }
        public void item(string item_no, string item_name,string SUPPLIERS_NAME)
        {
            textBox2.Text = item_no;
            textBox3.Text = item_name;
            textBox1.Text = SUPPLIERS_NAME;
        }
    }
}
