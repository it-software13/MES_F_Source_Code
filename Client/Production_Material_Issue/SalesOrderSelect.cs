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
    public partial class SalesOrderSelect : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
      
        public delegate void DataChangeHandler(object sender, DataChangeEventArgs args);
        public event DataChangeHandler DataChange;
        string condition1 = "M_Prod_Order"; 
        string value1;
        string sales_order_list = "";
        DataTable dt = new DataTable();

        
        public SalesOrderSelect()
        {
            InitializeComponent(); 
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void SalesOrderSelect_Load(object sender, EventArgs e)
        {
            GetSalesOrderList();
        }

        public void OnDataChange(object sender, DataChangeEventArgs args)
        {
            DataChange?.Invoke(this, args);
        }

       

        public class DataChangeEventArgs : EventArgs
        {
            public string value1 { get; set; }


            public DataChangeEventArgs(string s1 )
            {
                value1 = s1;
                
            }
        }


        public void GetSalesOrderList()
        {
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.F_WMS_Material_Issue_RTDM_Server", "GetMProdOrderList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = null;
            string textInfo = textBox1.Text;
            string filter = condition1 + " like '%" + textInfo + "%'";
            DataView dv = dt.DefaultView;
            dv.RowFilter = filter;
            dataGridView1.DataSource = dv;

            string sales_order_list = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["select"].Value))
                {
                    string sales_order = row.Cells[1].Value == null ? "" : row.Cells[1].Value.ToString();
                    // string stocName_codeList = row.Cells[2].Value == null ? "" : row.Cells[2].Value.ToString();
                    if (!"".Equals(sales_order))
                    {
                        if ("".Equals(sales_order_list))
                        {
                            sales_order_list += "'" + sales_order + "'";
                        }

                        else
                        {
                            sales_order_list += ",'" + sales_order + "'";
                        }

                    }
                }
            }
            value1 += sales_order_list;
            if (value1 == "")
            {
                //int index = dataGridView1.CurrentRow.Index;
                // value1 = dataGridView1.Rows[index].Cells[1].Value.ToString();
                //value2 = dataGridView1.Rows[index].Cells[2].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index > -1)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["select"].Value);
                    if (isSelected)
                    {
                        string sales_order = row.Cells["M_Prod_Order"].Value.ToString();
                        if (!"".Equals(sales_order))
                        {
                            if ("".Equals(sales_order_list))
                            {
                                sales_order_list += "'" + sales_order + "'";
                            }

                            else
                            {
                                sales_order_list += ",'" + sales_order + "'";
                            }

                        }

                        value1 = sales_order_list;
                        OnDataChange(this, new DataChangeEventArgs(value1));
                    }
                }

            }
            
            this.Close();
        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
           
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            style2.ForeColor = Color.Black;
            style2.BackColor = Color.LemonChiffon;

            if (e.RowIndex > -1)
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle = style2;
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            style1.ForeColor = Color.Azure;
            style1.BackColor = Color.LightSeaGreen;

            if (e.RowIndex > -1)
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle = style1;
        }
    }
}
