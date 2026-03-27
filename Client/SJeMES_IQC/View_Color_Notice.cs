using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;

namespace SJeMES_IQC
{
    public partial class View_Color_Notice : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public View_Color_Notice()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void View_Color_Notice_Load(object sender, EventArgs e)
        {
            Get_Color_Notice();
            checkBox1.Checked = false;
        }

        public void Get_Color_Notice()
        {
            string Input_Val = textBox1.Text;
            string start_date = string.Empty;
            string end_date = string.Empty;
            if (checkBox1.Checked)
            {
                start_date = dateTimePicker1.Text;
                end_date = dateTimePicker2.Text;
            }

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Input_Val", Input_Val);
            p.Add("start_date", start_date);
            p.Add("end_date", end_date);
            string retdata = WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",
                                        "SJeMES_IQC.IQC_ColorNotice",
                                        "Get_ColorNotice_byART",
                                        Program.Client.UserToken,
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
            }
            else
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["file_name"].Value = dr["file_name"].ToString(); 
                        dgvr.Cells["file_url"].Value = Program.Client.PicUrl + dr["file_url"].ToString();
                        //dgvr.Cells["fileurl"].Value = dr["file_url"].ToString();
                        i++;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Get_Color_Notice();
            checkBox1.Checked = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "operation")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("View"))
                        {
                            string file_url = Convert.ToString(dataGridView1.CurrentRow.Cells["file_url"].Value);
                            string file_name = Convert.ToString(dataGridView1.CurrentRow.Cells["file_name"].Value);
                            ShowFileHelper.ShowFile(file_url, file_name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }
}
