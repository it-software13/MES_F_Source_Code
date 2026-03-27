using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using SJeMES_Shared_Form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_IQC
{
    public partial class F_IQC_Customer_Complaint_Dispose_Inspection : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string PO_ORDER = string.Empty;
        public F_IQC_Customer_Complaint_Dispose_Inspection(string _PO)
        {
            InitializeComponent();
            PO_ORDER = _PO;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_IQC_Customer_Complaint_Dispose_Inspection_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetCmaTask_TaskList_Main;
            LoadPage();
        }

        /// <summary>
        /// 初始化分页
        /// </summary>
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// 查询-AQL任务清单
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetCmaTask_TaskList_Main(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("po", PO_ORDER);
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_CmaTask_TaskList",//类名
                                            "GetCmaTask_TaskList_Main",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["任务编号"].Value = dr["task_no"].ToString();
                        dgvr.Cells["PO"].Value = dr["po"].ToString();
                        dgvr.Cells["PO数量"].Value = dr["po_num"].ToString();
                        dgvr.Cells["分批数量"].Value = dr["lot_num"].ToString();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "查看报告")
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("task_no", dataGridView1.Rows[e.RowIndex].Cells["任务编号"].Value.ToString());
                    dic.Add("po", dataGridView1.Rows[e.RowIndex].Cells["PO"].Value.ToString());
                    dic.Add("num", dataGridView1.Rows[e.RowIndex].Cells["PO数量"].Value.ToString());
                    dic.Add("fpnum", dataGridView1.Rows[e.RowIndex].Cells["分批数量"].Value.ToString());

                    using (F_AQL_Aqlreport_New a = new F_AQL_Aqlreport_New(dic,Program.Client))
                    {
                        a.ShowDialog();
                    }
                }
            }
        }
    }
}
