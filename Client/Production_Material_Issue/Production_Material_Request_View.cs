using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutocompleteMenuNS;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.WebAPI;

namespace Production_Material_Issue
{
    public partial class Production_Material_Request_View : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public Production_Material_Request_View()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void btnrequest_Click(object sender, EventArgs e)
        {
            Production_Material_Request matreq = new Production_Material_Request();
            matreq.Show();
        }

        private void Production_Material_Request_View_Load(object sender, EventArgs e)
        {
            AutoSuggestDepartmentCode();
            autocompleteMenu2.SetAutocompleteMenu(textBox1, autocompleteMenu2);
            autocompleteMenu2.SetAutocompleteMenu(textBox2, autocompleteMenu2);
        }

        public void GetReq_MatList()
        {
            string S_Date = s_date.Value.ToString("yyyy/MM/dd");
            string E_Date = e_date.Value.ToString("yyyy/MM/dd");
            string Prod_Line = textBox1.Text;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("sdate", S_Date);
            p.Add("edate", E_Date);
            p.Add("Prod_Line", Prod_Line);
            p.Add("dept", "Prod");
            DataTable dt = new DataTable();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.F_WMS_Material_Issue_RTDM_Server", "GetReq_MatList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);

                if (dt!=null && dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found！");
                    dataGridView1.DataSource = null;
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            GetReq_MatList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string S_Date = dateTimePicker2.Value.ToString("yyyy/MM/dd");
            string E_Date = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            string Prod_Line = textBox2.Text;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("sdate", S_Date);
            p.Add("edate", E_Date);
            p.Add("Prod_Line", Prod_Line);
            p.Add("dept", "Reports");
            DataTable dt = new DataTable();
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.F_WMS_Material_Issue_RTDM_Server", "GetReq_MatList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dataGridView2.DataSource = dt;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found！");
                    dataGridView2.DataSource = null;
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        public void AutoSuggestDepartmentCode()
        {
            autocompleteMenu2.Items = null;
            autocompleteMenu2.MaximumSize = new Size(200, 200);
            var columnWidth = new[] { 10, 190 };
            int n = 1;
            DataTable dt2 = new DataTable();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.F_WMS_Material_Issue_RTDM_Server", "GetDeptCode", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt2.Rows[i]["department_code"].ToString() }, dt2.Rows[i]["department_code"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                        n++;
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Department not found！");
                    //MessageBox.Show("Material not found");
                }
            }
        }
        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            //style1.ForeColor = Color.Azure;
            //style1.BackColor = Color.LightSeaGreen;

            //if (e.RowIndex > -1)
            //    dataGridView1.Rows[e.RowIndex].DefaultCellStyle = style1;
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            //style2.ForeColor = Color.Black;
            //style2.BackColor = Color.LemonChiffon;

            //if (e.RowIndex > -1)
            //    dataGridView1.Rows[e.RowIndex].DefaultCellStyle = style2;
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
