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
using SJeMES_Framework.WebAPI;

namespace SJeMES_TSM
{
    public partial class Process_Selection : MaterialForm
    {
        public delegate void DataChangeHandler(object sender, DataChangeEventArgs args);
        public event DataChangeHandler DataChange;
        string ProcessType = string.Empty;
        string Model = string.Empty;
        string Process_List = string.Empty;
        string condition1 = "NAME";
        string value1;
        string Process_Names_List = "";
        DataTable dtJson1 = new DataTable();

        public Process_Selection()
        {
            InitializeComponent();
        }
        public Process_Selection(string processtype,string model,string process_list)
        {
            InitializeComponent();
            ProcessType = processtype;
            Model = model;
            Process_List = process_list;
        }

        public void OnDataChange(object sender, DataChangeEventArgs args)
        {
            DataChange?.Invoke(this, args);
        }

        public class DataChangeEventArgs : EventArgs
        {
            public string value1 { get; set; }


            public DataChangeEventArgs(string s1)
            {
                value1 = s1;

            }
        }

        private void Process_Selection_Load(object sender, EventArgs e)
        {
            GetProcessNames();
            List<string> stringList = Process_List.Split(new[] { "','" }, StringSplitOptions.None)
                               .Select(item => item.Trim('\''))
                               .ToList();
            int selectedCount = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string processName = row.Cells["Process_Name"].Value.ToString();
                if (stringList.Contains(processName))
                {
                    row.Cells["select"].Value = true;
                    selectedCount++;
                }
                else
                {
                    row.Cells["select"].Value = false; 
                }
            }
            txtcount.Text = selectedCount.ToString();
        }

        public void GetProcessNames()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("TYPE", ProcessType);
            retData.Add("Model", Model);
            string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                Program.Client.APIURL,
                "SJ_TSMAPI",
                "SJ_TSMAPI.Registration",
                "GetTypeOfProcess",
                Program.Client.UserToken,
                Newtonsoft.Json.JsonConvert.SerializeObject(retData)
            );
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                if (dtJson1.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtJson1;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found！");
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
            }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
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
                    string Process_Name = row.Cells["Process_Name"].Value.ToString();
                    if (!"".Equals(Process_Name))
                    {
                        if ("".Equals(Process_Names_List))
                        {
                            Process_Names_List += "'" + Process_Name + "'";
                        }

                        else
                        {
                            Process_Names_List += ",'" + Process_Name + "'";
                        }

                    }

                    value1 = Process_Names_List;
                    OnDataChange(this, new DataChangeEventArgs(value1));

                }
              }

            }
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string textInfo = textBox1.Text;
            string filter = condition1 + " like '%" + textInfo + "%'";
            DataView dv = dtJson1.DefaultView;
            dv.RowFilter = filter;
            dataGridView1.DataSource = dv;

            string Process_Names_List = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["select"].Value))
                {
                    string Process_Name = row.Cells[1].Value == null ? "" : row.Cells[1].Value.ToString();
                    // string stocName_codeList = row.Cells[2].Value == null ? "" : row.Cells[2].Value.ToString();
                    if (!"".Equals(Process_Name))
                    {
                        if ("".Equals(Process_Names_List))
                        {
                            Process_Names_List += "'"+Process_Name+"'";
                        }

                        else
                        {
                            Process_Names_List += ",'"+Process_Name+"'";
                        }

                    }
                }
            }
            value1 += Process_Names_List;
            if (value1 == "")
            {
                //int index = dataGridView1.CurrentRow.Index;
                // value1 = dataGridView1.Rows[index].Cells[1].Value.ToString();
                //value2 = dataGridView1.Rows[index].Cells[2].Value.ToString();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                if (e.ColumnIndex == dataGridView1.Columns["select"].Index)
                {
                    DataGridViewCheckBoxCell checkboxCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells["select"];
                    checkboxCell.Value = !Convert.ToBoolean(checkboxCell.Value);
                    int selectedCount = 0;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["select"].Value))
                        {
                            selectedCount++;
                        }
                    }

                    txtcount.Text = selectedCount.ToString();
                }
            
        }
    }
}
