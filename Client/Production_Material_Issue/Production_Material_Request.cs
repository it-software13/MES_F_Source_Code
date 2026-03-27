using AutocompleteMenuNS;
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
using static Production_Material_Issue.SalesOrderSelect;

namespace Production_Material_Issue
{
    public partial class Production_Material_Request : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
       
        public Production_Material_Request()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
          Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtdcode.Text)|| string.IsNullOrEmpty(txtMpo.Text)|| string.IsNullOrEmpty(txtmcode.Text)|| string.IsNullOrEmpty(txtmname.Text)|| string.IsNullOrEmpty(txtqty.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please fill all necessary details");
            }
            else
            {
                string Depart_Code = txtdcode.Text;
                string M_Prod_No = txtMpo.Text;
                string Material_Code = txtmcode.Text;
                string Material_Name = txtmname.Text;
                string Quantity = txtqty.Text;
                string Remarks = txtremarks.Text;
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("DCode", Depart_Code);
                p.Add("M_Prod_No", M_Prod_No);
                p.Add("MCode", Material_Code);
                p.Add("MName", Material_Name);
                p.Add("Qty", Quantity);
                p.Add("Remarks", Remarks);
                DataTable dt = new DataTable();
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.F_WMS_Material_Issue_RTDM_Server", "AddProdMaterialRequest", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Added Successfully！");
                    txtdcode.Text = "";
                    txtMpo.Text = "";
                    txtmcode.Text = "";
                    txtmname.Text = "";
                    txtqty.Text = "";
                    txtremarks.Text = "";
                }
                else
                {
                    string msg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    txtdcode.Text = "";
                    txtMpo.Text = "";
                    txtmcode.Text = "";
                    txtmname.Text = "";
                    txtqty.Text = "";
                    txtremarks.Text = "";
                }

            } 
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            txtdcode.Text = "";
            txtMpo.Text = "";
            txtmcode.Text = "";
            txtmname.Text = "";
            txtqty.Text = "";
            txtremarks.Text = "";
        }

        private void Production_Material_Request_Load(object sender, EventArgs e)
        {
            AutoSuggestDepartmentCode();
            AutoSuggestMaterialCode();
            autocompleteMenu1.SetAutocompleteMenu(txtmcode, autocompleteMenu1); 
            autocompleteMenu2.SetAutocompleteMenu(txtdcode, autocompleteMenu2);
            txtMpo.ReadOnly = false;
        }

        public void AutoSuggestMaterialCode()
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new Size(200, 200);
            var columnWidth = new[] { 10, 190 };
            int n = 1;
            DataTable dt1 = new DataTable();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.F_WMS_Material_Issue_RTDM_Server", "GetMaterialCode", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt1.Rows[i]["item_no"].ToString() }, dt1.Rows[i]["item_no"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                        n++;
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Material not found！");
                    //MessageBox.Show("Material not found");
                }
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

        private void button1_Click(object sender, EventArgs e) 
        {
            SalesOrderSelect frm = new SalesOrderSelect();
            frm.DataChange += new SalesOrderSelect.DataChangeHandler(DataChanged_txtMpo);
            frm.ShowDialog();
        }

        public void DataChanged_txtMpo(object sender, DataChangeEventArgs args)
        {
            txtMpo.Text = args.value1;
            //txtso.ReadOnly = true;
            //GetSTOC_TYPE(Convert.ToString(cbOrg.SelectedValue), textWarehouse1.Text, cbSTOCType);
        }

        private void txtqty_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtqty.Text))
            {
                return;
            }
            if (txtqty.Text == "0")
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Quantity Cannot be Zero");
                txtqty.Text = "";
                return;
            }
            if (string.IsNullOrEmpty(txtdcode.Text) || string.IsNullOrEmpty(txtMpo.Text) || string.IsNullOrEmpty(txtmcode.Text) || string.IsNullOrEmpty(txtmname.Text) || string.IsNullOrEmpty(txtqty.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please fill all necessary details");
                txtqty.Text = "";
            }

            else
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("M_Po_Num", txtMpo.Text);
                p.Add("MCode", txtmcode.Text);
                p.Add("Qty", txtqty.Text);
                DataTable dt = new DataTable();
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.F_WMS_Material_Issue_RTDM_Server", "CheckAvailableQty", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                if (!Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string msg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    txtqty.Text = "";
                }
            }


        }

        private void txtmcode_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtmcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("MCode", txtmcode.Text);

                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.F_WMS_Material_Issue_RTDM_Server", "GetMatName", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    txtmname.Text = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();

                }
                else
                {
                    string msg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
