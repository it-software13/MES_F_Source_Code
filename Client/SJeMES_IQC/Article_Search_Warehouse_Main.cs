using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
namespace SJeMES_IQC
{
    public partial class Article_Search_Warehouse_Main : Form
    {
        public Article_Search_Warehouse_Main()
        {
            InitializeComponent();


            textBox1.CharacterCasing = CharacterCasing.Upper;
            textBox1.Multiline = true;
            textBox1.AcceptsReturn = true;
            textBox1.AcceptsTab = false;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.WordWrap = false;
            textBox1.Font = new Font("Consolas", 10, FontStyle.Regular);
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox1.Padding = new Padding(5);
            textBox1.MinimumSize = new Size(300, 40);
        }

        private void Btn_search_Click(object sender, EventArgs e)
        {

            if (art_text.Text == "")
            {
                MessageHelper.ShowErr(this, "Please Enter Article!");
            }
            else if(textBox1.Text == "")
            {
                MessageHelper.ShowErr(this, "Please Enter Purchase orders!");
            }
            else
            {

                string retdata = GetArticleDetails();

                ResultObject ret =
                    JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                DataTable dt =
                    JsonConvert.DeserializeObject<DataTable>(ret.RetData);

                
                if (dt == null || dt.Rows.Count == 0)
                {
                    dataGridView1.DataSource = null;  
                    MessageHelper.ShowErr(this, "No data Found!");
                    return;
                }

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["ART_NO"].DisplayIndex = 0;
                dataGridView1.Columns["SHOE_NO"].DisplayIndex = 1;
                dataGridView1.Columns["ORDER_NO"].DisplayIndex = 2;
                dataGridView1.Columns["ITEM_NO"].DisplayIndex = 3;
                dataGridView1.Columns["NAME_T"].DisplayIndex = 4;
                dataGridView1.Columns["ORD_QTY"].DisplayIndex = 5;
                dataGridView1.Columns["VEND_NO"].DisplayIndex = 6;
                dataGridView1.Columns["SUPPLIERS_NAME"].DisplayIndex = 7;
                dataGridView1.Columns["PART_NO"].DisplayIndex = 8;
              

            }
        }

        private string GetArticleDetails()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("art_name", art_text.Text);
            string inputText = textBox1.Text.Trim();
            List<string> POList = inputText
.Split(new[] { ',', ';', '\n', '\r', ' ' }, StringSplitOptions.RemoveEmptyEntries)
.Select(a => a.Trim())
.Distinct()
.ToList();
            List<string> POvalues = POList;
            p.Add("orders", POvalues);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",
                                        "SJeMES_IQC.VMaterialinventory",
                                        "GetArticleDetailsData",
                                        Program.Client.UserToken,
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            return retdata;
        }
    }
}
