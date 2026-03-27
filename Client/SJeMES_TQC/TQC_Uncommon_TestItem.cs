using MaterialSkin;
using MaterialSkin.Controls;
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

namespace SJeMES_TQC
{
    public partial class TQC_Uncommon_TestItem : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string workshop_section_no = string.Empty;
        string task_no = string.Empty;
        private TQC_Task_Edit tqc;
        DataTable NotInDt = new DataTable();
        public TQC_Uncommon_TestItem(string _task_no, string _workshop_section_no,TQC_Task_Edit _tqc,DataTable _NotInDt)
        {
            InitializeComponent();
            task_no = _task_no;
            workshop_section_no = _workshop_section_no;
            tqc = _tqc;
            NotInDt = _NotInDt;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// tqc编辑页面不常见项目查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetTQC_Uncommon_TestItem(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("task_no", task_no);
                data.Add("workshop_section_no", workshop_section_no);
                data.Add("keyvalue", textBox1.Text.Trim());
                data.Add("NotInDt", NotInDt);
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TQCAPI",//类库名
                                            "SJ_TQCAPI.TQC_Task",//类名
                                            "GetTQC_Uncommon_TestItem",//方法名
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
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["inspection_code"].Value = dr["inspection_code"].ToString();
                        dgvr.Cells["inspection_name"].Value = dr["inspection_name"].ToString();
                        dgvr.Cells["qc_type"].Value = dr["qc_type"].ToString();
                        dgvr.Cells["judgment_criteria"].Value = dr["judgment_criteria"].ToString();
                        dgvr.Cells["standard_value"].Value = dr["standard_value"].ToString();
                        dgvr.Cells["shortcut_key"].Value = dr["shortcut_key"].ToString();
                        if (NotInDt.Rows.Count > 0)
                        {
                            foreach (DataRow item in NotInDt.Rows)
                            {
                                if (dr["inspection_code"].ToString()==item["inspection_code"].ToString())
                                {
                                    dataGridView1.Rows.RemoveAt(i);
                                }
                            }
                        }
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

        private void TQC_Uncommon_TestItem_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetTQC_Uncommon_TestItem;
            LoadPage();
            this.dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                DataGridView dataGridView1 = (DataGridView)sender;
                if (dataGridView1.Columns[e.ColumnIndex].Name == "cz")
                {
                    DataTable bcjdt = new DataTable();
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        bcjdt.Columns.Add(dataGridView1.Columns[i].Name);
                    }
                    bcjdt.Rows.Add();
                    foreach (DataRow item in bcjdt.Rows)
                    {
                        item["inspection_code"] = dataGridView1.Rows[e.RowIndex].Cells["inspection_code"].Value;
                        item["inspection_name"] = dataGridView1.Rows[e.RowIndex].Cells["inspection_name"].Value;
                        item["qc_type"] = dataGridView1.Rows[e.RowIndex].Cells["qc_type"].Value;
                        item["judgment_criteria"] = dataGridView1.Rows[e.RowIndex].Cells["judgment_criteria"].Value;
                        item["standard_value"] = dataGridView1.Rows[e.RowIndex].Cells["standard_value"].Value;
                        item["shortcut_key"] = dataGridView1.Rows[e.RowIndex].Cells["shortcut_key"].Value;
                    }
                    tqc.Uncommon_TestItem_RowsAdd(bcjdt);
                    this.Close();
                }
            }
        }
    }
}
