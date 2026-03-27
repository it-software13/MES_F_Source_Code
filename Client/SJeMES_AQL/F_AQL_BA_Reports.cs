using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.WebAPI;
using NewExportExcels;

namespace SJeMES_AQL
{
    public partial class F_AQL_BA_Reports : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_AQL_BA_Reports()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
        }

        private void F_AQL_BA_Reports_Load(object sender, EventArgs e)
        {
          
        }

        public void Get_BA_Reports()
        {
            if (!checkBox1.Checked)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select the Date range");
                return;
            }
            string plant = comboBox1.Text;
            string s_date = dateTimePicker1.Text;
            string e_date = dateTimePicker2.Text;

            Dictionary<string, object> p = new Dictionary<string, object>(); 
            p.Add("plant", plant);
            p.Add("s_date", s_date);
            p.Add("e_date", e_date);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                             Program.Client.APIURL,
                                             "SJ_AQLAPI",//类库名
                                             "SJ_AQLAPI.AQL_BA_Entry",//类名
                                             "Get_BA_Reports",//方法名
                                             Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            dataGridView1.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    dataGridView1.Rows.Add();
                    DataGridViewRow dgvr = dataGridView1.Rows[i];
                    dgvr.Cells["date"].Value = dr["create_date"].ToString();
                    dgvr.Cells["model"].Value = dr["model_name"].ToString();
                    dgvr.Cells["art"].Value = dr["article"].ToString();
                    //dgvr.Cells["category"].Value = dr["id"].ToString();
                    dgvr.Cells["po"].Value = dr["po"].ToString();
                    dgvr.Cells["line"].Value = dr["production_line"].ToString();
                    dgvr.Cells["po_size"].Value = dr["Po_num"].ToString();
                    dgvr.Cells["pairs_inspected"].Value = dr["pairs_inspected"].ToString();
                    dgvr.Cells["pairs_beautiful"].Value = dr["pairs_beautiful"].ToString();
                    dgvr.Cells["percentage"].Value = dr["beautiful_rate"].ToString();
                    dgvr.Cells["star_rating"].Value = dr["star_rating"].ToString();
                    dgvr.Cells["c1"].Value = dr["C1"].ToString();
                    dgvr.Cells["c2"].Value = dr["C2"].ToString();
                    dgvr.Cells["c3"].Value = dr["C3"].ToString();
                    dgvr.Cells["c4"].Value = dr["C4"].ToString();
                    dgvr.Cells["c5"].Value = dr["C5"].ToString();
                    dgvr.Cells["c6"].Value = dr["C6"].ToString();
                    dgvr.Cells["c7"].Value = dr["C7"].ToString();
                    dgvr.Cells["c8"].Value = dr["C8"].ToString();
                    dgvr.Cells["c9"].Value = dr["C9"].ToString();
                    dgvr.Cells["bad_item_names"].Value = dr["bad_item_names"].ToString();
                    i++;
                }
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            Get_BA_Reports();
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");   
            }
            else
            {
                string a = "BA_Report.xls";
                ExportExcels.Export(a, dataGridView1);
            }
        }
    }
}
