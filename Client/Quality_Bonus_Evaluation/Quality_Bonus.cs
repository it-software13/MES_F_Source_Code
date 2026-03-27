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
using SJeMES_Framework.Common;

namespace Quality_Bonus_Evaluation
{
    public partial class Quality_Bonus : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public Quality_Bonus()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
        }

        private void Quality_Bonus_Load(object sender, EventArgs e)
        {
          // Get_Quality_Bonus();
        }
       
        public void Get_Quality_Bonus()
        {
            string PO = textBox2.Text;
            string ART = textBox3.Text;
            string Department = textBox1.Text;
            string Group = textBox4.Text;
            string s_date = dateTimePicker1.Text;
            string e_date = dateTimePicker2.Text;

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("PO", PO);
            p.Add("ART", ART);
            p.Add("Department", Department);
            p.Add("Group", Group);
            p.Add("s_date", s_date);
            p.Add("e_date", e_date);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                             Program.Client.APIURL,
                                             "SJ_TQCAPI",//类库名
                                             "SJ_TQCAPI.TQC_Task",//类名
                                             "Get_Quality_Bonus",//方法名
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
                    dgvr.Cells["Prod_line"].Value = dr["production_line_code"].ToString();
                    dgvr.Cells["insp_num"].Value = dr["toal_inspected"].ToString();
                    dgvr.Cells["first_pass"].Value = dr["first_pass"].ToString();
                    dgvr.Cells["b_product_qty"].Value = dr["b_grade"].ToString();
                    dgvr.Cells["rft"].Value = Math.Round((Convert.ToDecimal(dr["rft_pass_percent"]) * 100), 2).ToString();
                    dgvr.Cells["b_grade_percentage"].Value = Math.Round((Convert.ToDecimal(dr["b_grade_percentage"]) * 100), 2).ToString();

                    double rft = Convert.ToDouble(dgvr.Cells["rft"].Value);
                    double b_grade_percentage = Convert.ToDouble(dgvr.Cells["b_grade_percentage"].Value);
                    double RePacking = 0;
                    //dgvr.Cells["rft"].Value = Math.Round(rft * 100, 2).ToString() + "%";
                    //dgvr.Cells["b_grade_percentage"].Value = Math.Round(b_grade_percentage * 100, 2).ToString() + "%";

                    if (dgvr.Cells["Prod_line"].Value != null)
                    {
                        string cellValue = dgvr.Cells["Prod_line"].Value.ToString();

                        // Extract "APC" from the string
                        string extractedAP = cellValue.Substring(4, 3); // Assumes "APC" is always at the same position

                        if (extractedAP == "AP1" || extractedAP == "AP2" || extractedAP == "AP3" || extractedAP == "AP5" || extractedAP == "AP6" || extractedAP == "AP7" || extractedAP == "AP8")
                        {
                            dgvr.Cells["bonus"].Value = 0; // Default value
                            dgvr.Cells["repacking"].Value = 0;

                            if (rft >= 85 && b_grade_percentage <= 0.025 && RePacking <= 1.15)
                            {
                                //bonusAmount = 75;
                                dgvr.Cells["bonus"].Value = 75;
                            }
                            else if (rft >= 82 && rft < 84 && b_grade_percentage <= 0.028 && RePacking <= 1.20)
                            {
                                //bonusAmount = 65;
                                dgvr.Cells["bonus"].Value = 65;
                            }
                            else if (rft >= 79 && rft < 81 && b_grade_percentage <= 0.030 && RePacking <= 1.35)
                            {
                                //bonusAmount = 55;
                                dgvr.Cells["bonus"].Value = 55;
                            }
                            else if (rft >= 77 && rft < 79 && b_grade_percentage <= 0.035 && RePacking <= 1.40)
                            {
                                //bonusAmount = 45;
                                dgvr.Cells["bonus"].Value = 45;
                            }
                            else if (rft >= 74 && rft < 76 && b_grade_percentage <= 0.040 && RePacking <= 1.45)
                            {
                                //bonusAmount = 35;
                                dgvr.Cells["bonus"].Value = 35;
                            }
                            else if (rft >= 70 && rft < 74 && b_grade_percentage <= 0.050 && RePacking <= 1.50)
                            {
                                //bonusAmount = 25;
                                dgvr.Cells["bonus"].Value = 25;
                            }
                            else if (rft >= 67 && rft < 70 && b_grade_percentage <= 0.060 && RePacking <= 1.60)
                            {
                                //bonusAmount = 15;
                                dgvr.Cells["bonus"].Value = 15;
                            }
                            else if (rft >= 65 && rft < 67 && b_grade_percentage <= 0.070 && RePacking <= 1.70)
                            {
                                //bonusAmount = 5;
                                dgvr.Cells["bonus"].Value = 5;
                            }

                            // dgvr.Cells["Bonus"].Value = bonusAmount;  
                        }
                    }
                   i++;
                }
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            Get_Quality_Bonus();
        }
    }
}
