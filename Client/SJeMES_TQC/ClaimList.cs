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
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.WebAPI;
using SJeMES_IQC;

namespace SJeMES_TQC
{
    public partial class ClaimList : MaterialForm
    {
        DataTable dt;
        public ClaimList(DataTable _dt)
        {
            InitializeComponent(); 
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            dt = _dt; 
        }

        private void ClaimList_Load(object sender, EventArgs e)
        {
            LoadList(dt);
        }

        public void LoadList(DataTable dt)
        {
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                dataGridView1.Rows.Add();
                DataGridViewRow dgvr = dataGridView1.Rows[i];
                dgvr.Cells["claimdate"].Value = dr["COMPLAINT_DATE"].ToString(); 
                dgvr.Cells["complaint_no"].Value = dr["COMPLAINT_NO"].ToString(); 
                dgvr.Cells["status"].Value = dr["STATUS"].ToString();
                dgvr.Cells["fileguid"].Value = dr["imglist"].ToString();
                i++;
            }

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
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "operate")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operate"] as DataGridViewOperationCell;
                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("view"))
                        {
                            string COMPLAINT_DATE = Convert.ToString(dataGridView1.CurrentRow.Cells["claimdate"].Value);
                            string COMPLAINT_NO = Convert.ToString(dataGridView1.CurrentRow.Cells["complaint_no"].Value); 
                            string STATUS = Convert.ToString(dataGridView1.CurrentRow.Cells["status"].Value);
                            string imglist= Convert.ToString(dataGridView1.CurrentRow.Cells["fileguid"].Value);

                            var currRowFileDt = Getimage_guid(imglist);

                            ClainListView clv = new ClainListView(currRowFileDt);
                            clv.ShowDialog(); 
                             
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
        public static DataTable Getimage_guid(string image_guid)
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("image_guid", image_guid);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.IQC_Customer_Complaint",//类名
                                        "Getimage_guid",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }

            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //视图数据显示

            //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("net_file_url", typeof(string));
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["file_url"].ToString()))
                    {
                        try
                        {
                            dr["net_file_url"] = Program.Client.PicUrl + dr["file_url"].ToString();
                        }
                        catch
                        {
                        }
                    }
                    i++;
                }
            }
            return dt;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
