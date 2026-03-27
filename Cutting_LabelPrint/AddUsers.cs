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

namespace Cutting_LabelPrint
{
    public partial class AddUsers : Form
    {
        public AddUsers()
        {
            InitializeComponent();
        }

        private void AddUsers_Load(object sender, EventArgs e)
        {

        }

        private void Barcode_Click(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e) 
        {
            Dictionary<string , object > dic = new Dictionary<string , object>();
            string barcode = txt_barcode.Text; 
            string department = txt_dpt.Text;
            dic.Add("barcode" , barcode); 
            dic.Add("department" , department);
            string ret = WebAPIHelper.Post(Program.client.APIURL,
                         "KZ_RTLAPI",
                         "KZ_RTLAPI.Controllers.CuttingLabelServer",
                         "AddUsers_Cut_dlt_fun",
                         Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dic));
            Cursor.Current = Cursors.Default; 
            if(Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string , object>>(ret)["IsSuccess"]))
            {
                // Deserialize the JSON response into a Dictionary<string, object>
                var responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                var retdata = responseData["RetData"];

                if (string.Equals(retdata.ToString(), "Data successfully inserted!", StringComparison.OrdinalIgnoreCase))
                {
                    txt_barcode.Text = "";
                    txt_dpt.Text = ""; 
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "User Added Successfully");
                     
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "User Already Exist");
                }
 
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "User Not Added , Contact IT "); 
            } 
        }  

        private void txt_dpt_TextChanged(object sender, EventArgs e)
        {
            txt_dpt.Text = txt_dpt.Text.ToUpper();
            txt_dpt.SelectionStart = txt_dpt.Text.Length;
        }
    }
}
