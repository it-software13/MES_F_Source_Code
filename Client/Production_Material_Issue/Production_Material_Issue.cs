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

namespace Production_Material_Issue
{
    public partial class Production_Material_Issue : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public Production_Material_Issue()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void btn_search_Click(object sender, EventArgs e)
        { 
            GetReq_MatList();
        }

        public void GetReq_MatList() 
        {
            string S_Date = s_date2.Value.ToString("yyyy/MM/dd");
            string E_Date = e_date2.Value.ToString("yyyy/MM/dd");
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("sdate", S_Date);
            p.Add("edate", E_Date);
            p.Add("dept", "Wh");
            DataTable dt = new DataTable();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.F_WMS_Material_Issue_RTDM_Server", "GetReq_MatList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                if (dt.Rows.Count > 0)
                {
                   dataGridView1.DataSource = dt;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found！");
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int WH_QTY;
                  if (dataGridView1.Columns[e.ColumnIndex].Name == "SUBMIT")
                  {
                    if(string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells["WH_ISSUED_QTY"].Value.ToString()))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter Issue Quantity");
                        return;
                    }
                    bool isNumber = int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["WH_ISSUED_QTY"].Value.ToString(), out WH_QTY); 
                    if (!isNumber)
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please check the Quantity");
                        return;
                    }
                    if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["WH_ISSUED_QTY"].Value)> Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["REQUIRED_QTY"].Value))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Issue Quantity is not more than Request Quantity");
                        return;
                    }

                        DialogResult dr = MessageBox.Show("Are you sure you want to Submit!", "Prompt", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (dr == DialogResult.OK)
                        {
                       
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            string MAIN_ID = dataGridView1.Rows[e.RowIndex].Cells["MAIN_ID"].Value.ToString();
                            string WH_ISSUED_QTY = dataGridView1.Rows[e.RowIndex].Cells["WH_ISSUED_QTY"].Value.ToString();
                            string WH_REMARKS = dataGridView1.Rows[e.RowIndex].Cells["WH_REMARKS"].Value.ToString(); 
                            p.Add("MAIN_ID", MAIN_ID);
                            p.Add("WH_ISSUED_QTY", WH_ISSUED_QTY);
                            p.Add("WH_REMARKS", WH_REMARKS);
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                        Program.Client.APIURL,
                                                        "KZ_WMSAPI",
                                                        "KZ_WMSAPI.Controllers.F_WMS_Material_Issue_RTDM_Server", 
                                                        "UpdateProdMaterialRequest",//方法名
                                                        Program.Client.UserToken,//token
                                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (ret.IsSuccess)
                            {
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully Added"); 

                            }
                        GetReq_MatList();
                    }
                  }
                     
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string S_Date = dateTimePicker2.Value.ToString("yyyy/MM/dd");
            string E_Date = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("sdate", S_Date);
            p.Add("edate", E_Date);
            p.Add("dept", "Reports");
            DataTable dt = new DataTable();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.F_WMS_Material_Issue_RTDM_Server", "GetReq_MatList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                if (dt.Rows.Count > 0)
                {
                    dataGridView2.DataSource = dt;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found！");
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

       

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            //style2.ForeColor = Color.Black;
            //style2.BackColor = Color.LemonChiffon;

            //if (e.RowIndex > -1)
            //    dataGridView1.Rows[e.RowIndex].DefaultCellStyle = style2;
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            //style1.ForeColor = Color.Azure;
            //style1.BackColor = Color.LightSeaGreen;

            //if (e.RowIndex > -1)
            //    dataGridView1.Rows[e.RowIndex].DefaultCellStyle = style1;
        }

        private void dataGridView2_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            //style2.ForeColor = Color.Black;
            //style2.BackColor = Color.LemonChiffon;

            //if (e.RowIndex > -1)
            //    dataGridView2.Rows[e.RowIndex].DefaultCellStyle = style2;
        }

        private void dataGridView2_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            //style1.ForeColor = Color.Azure;
            //style1.BackColor = Color.LightSeaGreen;

            //if (e.RowIndex > -1)
            //    dataGridView2.Rows[e.RowIndex].DefaultCellStyle = style1;
        }
    }
}
