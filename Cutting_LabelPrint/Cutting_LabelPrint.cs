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
    public partial class Cutting_LabelPrint : Form
    {
        public Cutting_LabelPrint() 
        {
            InitializeComponent();
            HideDltBtn();
            HideRegBtn();  
        }
        private void HideDltBtn() 
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();  
            string ret = WebAPIHelper.Post(Program.client.APIURL,  
                         "KZ_RTLAPI",  
                         "KZ_RTLAPI.Controllers.CuttingLabelServer",  
                         "CheckUserVerification",  
                         Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(parm)); 
            Cursor.Current = Cursors.Default;
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
               // DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if(json == "[]") 
                {
                    this.btn_delete.Enabled = false;
                } 
                else 
                { 
                    this.btn_delete.Enabled = true;
                }  
            }  
        }    
        private void HideRegBtn() 
        {
            Dictionary<string , object> parm = new Dictionary<string , object>();
            string ret = WebAPIHelper.Post(Program.client.APIURL,
                          "KZ_RTLAPI",
                         "KZ_RTLAPI.Controllers.CuttingLabelServer",
                         "CheckUserVerification_Register", 
                         Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(parm)); 
            Cursor.Current = Cursors.Default;
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                
                if(json == "Success")
                {
                    this.Register.Enabled = true; 
                } 
                else
                {
                    this.Register.Enabled = false; 
                }
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)   
        {
            dataGridView1.DataSource = null; 
            if(string.IsNullOrEmpty(txt_MasterPO.Text)) 
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this,"Please Enter Master PO"); 
                return;
            } 

            if (string.IsNullOrEmpty(txt_PartNo.Text)) 
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Part NO"); 
                return; 
            }  

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Mpo",txt_MasterPO.Text);
            dic.Add("Pno",txt_PartNo.Text);
            Cursor.Current = Cursors.WaitCursor;
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.CuttingLabelServer", "GetCuttingLabelData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dic));
            Cursor.Current = Cursors.Default;
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {                                 string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();    
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dataGridView1.DataSource = dtJson;
                if(dtJson.Rows.Count > 0)
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this , "Data binded successfully");
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data"); 
                }
            }  

            else 
            { 
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }  
        }  

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($@"Are you sure to {btn_delete.Text} data?" , "Confirm Action" , MessageBoxButtons.YesNo , MessageBoxIcon.Question);         
            if(result == DialogResult.Yes)
            {
                DeleteFunction(btn_delete.Text); 
            } 
        }          
        
        public void DeleteFunction(string function)  
        {
            DataTable dt = new DataTable();
            if(dataGridView1.Rows.Count > 0)
            {
                foreach(DataGridViewColumn column in dataGridView1.Columns)
                {
                    dt.Columns.Add(column.Name); 
                } 
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {      
                    bool isSelected = Convert.ToBoolean(row.Cells["Select"].Value);
                    if(isSelected)
                    {
                        DataRow drow = dt.NewRow();
                        foreach(DataGridViewCell cell in row.Cells)
                        {
                            drow[cell.ColumnIndex] = cell.Value;
                        }
                        dt.Rows.Add(drow);
                    } 
                } 
                if(dt.Rows.Count > 0) 
                {
                    Dictionary<string , object > dict = new Dictionary<string , object>();
/*                    dict.Add("ORDER_NO", ORDER_NO);
*/                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                      dict.Add("DataTable", jsonData);
                      
                    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                             "KZ_RTLAPI",
                             "KZ_RTLAPI.Controllers.CuttingLabelServer",
                             "DeleteCuttingLabelData",
                             Program.client.UserToken,
                             Newtonsoft.Json.JsonConvert.SerializeObject(dict)); 
                    ResultObject Result = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(ret);
                    if(Result.IsSuccess)
                    {
                        btn_Search_Click(null, EventArgs.Empty);  
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Success");  
                    } 
                    else 
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this , "Error: Data cannot be deleted as there is related data present.");
                    }        

                } 
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data selected"); 
                }
            } 
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found"); 
            }
        } 


        private void selectallbutton_Click(object sender, EventArgs e)
        {
            if(dataGridView1.RowCount > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells["Select"].Value = true;
                }
            } else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found"); 
            }
           
        }                                      

        private void deselectbutton_Click(object sender, EventArgs e)
        {
            if(dataGridView1.RowCount > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells["Select"].Value = false;
                }
            } else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found"); 
            } 
           
        } 

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           if( dataGridView1.Columns[e.ColumnIndex].Name == "Select" )
            {
                bool value = dataGridView1.Rows[e.RowIndex].Cells["Select"].Value as bool? ?? false; 
                if (value)
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Select"].Value = false;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Select"].Value = true;
                }
            }
        }

        private void Register_Click(object sender, EventArgs e) 
        {
            AddUsers form2 = new AddUsers();
            form2.ShowDialog();
        }
    }
}
