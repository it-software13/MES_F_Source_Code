using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using NewExportExcels;
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;

namespace SJeMES_TSM
{
    public partial class Termination_Emp_List : MaterialForm
    {
        public Termination_Emp_List()
        {
            InitializeComponent();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            Termination_Emp_Skill_Data();
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "Termination_Employee_Details.xls";
                ExportExcels.Export(a, dataGridView1);
            }
        }

        public void Termination_Emp_Skill_Data()
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Process Type");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            string Barcode = txtbcode.Text; 
            string Process = comboBox1.Text;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Barcode", Barcode); 
            p.Add("Process", Process);
            string responseData = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TSMAPI", "SJ_TSMAPI.Termination_Emp_List", "Termination_Emp_Skill_Data", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dtJson1.Rows.Count > 0)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dtJson1;
                }
                else
                {
                    dataGridView1.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Available");
                }

            }
        }

        
        public void Import_Termination_Emp_List()
        {
            Cursor.Current = Cursors.WaitCursor;
            Termination_Emp ter_emp = new Termination_Emp();
            DataTable dt = ter_emp.Import_Termination_Emp_List(dt_month.Text);
            if(dt.Rows.Count>0)
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("Data", dt);
                string responseData = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TSMAPI", "SJ_TSMAPI.Termination_Emp_List", "Import_Termination_Emp_List", Program.Client.UserToken, JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
                if (ret.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, $@"{dt.Rows.Count} Records are Updated");
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, $@"{dt.Rows.Count} Records are Updated");
            }
            
        }




        private void btn_terminate_Click(object sender, EventArgs e)
        {
            Import_Termination_Emp_List();
        }
    }
}
