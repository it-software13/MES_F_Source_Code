using SJeMES_BDM.Forms;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_BDM
{
    public partial class FrmFileList : Form
    {
        public FrmFileList()
        {
            InitializeComponent();
        }

        public string prod_no=string.Empty;
        public string general_testtype_no = string.Empty;
        public string category_no = string.Empty;
        public string testitem_code = string.Empty;
        public  void GetData(string prod_no,string general_testtype_no,string category_no,string testitem_code)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("prod_no", prod_no);
            p.Add("general_testtype_no", general_testtype_no);
            p.Add("category_no", category_no);
            p.Add("testitem_code", testitem_code);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.InspectionTableView",//类名
                                        "GET_PROD_File_List",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata.ToString());
            DataTable dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["RetData1"].ToString());
            if (dt1.Rows.Count!=0||dt1!=null)
            {
                int a = 0;
                foreach (DataRow item in dt1.Rows)
                {
                    dataGridView1.Rows.Add();
                    DataGridViewRow dgvr = dataGridView1.Rows[a];
                    dgvr.Cells["FILE_NAME"].Value = item["FILE_NAME"].ToString();
                    dgvr.Cells["FILE_URL"].Value = item["FILE_URL"].ToString();
                    a++;
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");
                this.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btncz")
            {
                string URL = Program.Client.PicUrl + dataGridView1.Rows[e.RowIndex].Cells["FILE_URL"].Value.ToString().Replace("/wwwroot", "");
                FrmShowFiles frmShowFiles = new FrmShowFiles(URL);
                frmShowFiles.ShowDialog();
            }
           
        }
        private void F_BDM_ProdCustomQuality_LookFile_Load(object sender, EventArgs e)
        {
            GetData(prod_no,general_testtype_no,category_no,testitem_code);
        }
    }
}
