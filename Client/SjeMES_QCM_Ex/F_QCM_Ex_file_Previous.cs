using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
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

namespace SjeMES_QCM_Ex
{
    public partial class F_QCM_Ex_file_Previous : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        
        string Task_No;
        public F_QCM_Ex_file_Previous(string TASK_NO)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            Task_No = TASK_NO;
            GetDataList(Task_No);
        }
        //public void FormLoad()
        //{
        //    pageControl1.PageSize = 15;
        //    pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
        //    pageControl1.SetPage();
        //}
        //private void F_QCM_Ex_file_Previous_Load(object sender, EventArgs e)
        //{
        //    FormLoad();
        //    //pageControl1.BindPageEvent += GetDataList;
        //}

        public void GetDataList(string Task_No)
        {
            //totalCount = 0;
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("Task_No", Task_No);
               // p.Add("pageSize", pageSize);
                //p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJ_QCMAPI",//类库名
                                                "SJ_QCMAPI.ExShose",//类名
                                                "GetExARCList_Previous",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        int i = dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells["labno"].Value = dr["task_no"].ToString();
                        dataGridView1.Rows[i].Cells["objectname"].Value = dr["task_name"].ToString();
                        dataGridView1.Rows[i].Cells["location"].Value = dr["stock_code"].ToString();
                        dataGridView1.Rows[i].Cells["art"].Value = dr["art_no"].ToString();
                        dataGridView1.Rows[i].Cells["color"].Value = dr["colour_type"].ToString();
                        dataGridView1.Rows[i].Cells["indate"].Value = dr["warehousing_date"].ToString();
                        dataGridView1.Rows[i].Cells["reviewdate"].Value = dr["latest_review_date"].ToString();
                        dataGridView1.Rows[i].Cells["remarks"].Value = dr["remarks"].ToString();
                        dataGridView1.Rows[i].Cells["createdby"].Value = dr["createdby"].ToString();
                    }
                }
                //totalCount = int.Parse(dic["rowCount"].ToString());
                this.dataGridView1.ClearSelection();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
