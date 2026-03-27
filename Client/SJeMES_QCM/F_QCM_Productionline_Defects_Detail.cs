using DataGrid.DataGridViewCustomColumn;
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

namespace SJeMES_QCM
{
    public partial class F_QCM_Productionline_Defects_Detail : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public DataTable _dt { get; set; }
        public F_QCM_Productionline_Defects_Detail(DataTable dt)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            _dt = dt;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_Productionline_Defects_Edit_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetDataList;

            if (_dt.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in _dt.Rows)
                {
                    dataGridView1.Rows.Add();
                    DataGridViewRow dgvr = dataGridView1.Rows[i];
                    txt_depart_no.Text = dr["department_no"].ToString();
                    txt_depart_name.Text = dr["department_name"].ToString();
                    txt_pro_no.Text = dr["productionline_no"].ToString();
                    txt_pro_name.Text = dr["productionline_name"].ToString();

                    i++;
                }
            }

            FormLoad();
        }

        public void FormLoad()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            F_QCM_Productionline_Defects_Add add = new F_QCM_Productionline_Defects_Add(_dt);
            add.ShowDialog();
            FormLoad();
        }

        /// <summary>
        /// 不良问题新增展示
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();

                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in _dt.Rows)
                    {
                        p.Add("department_no", dr["department_no"].ToString());
                        p.Add("department_name", dr["department_name"].ToString());
                        p.Add("productionline_no", dr["productionline_no"].ToString());
                        p.Add("productionline_name", dr["productionline_name"].ToString());
                    }
                    p.Add("pageSize", pageSize);
                    p.Add("pageIndex", pageIndex);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJ_QCMAPI",//类库名
                                                "SJ_QCMAPI.Quality_DepartmentBase",//类名
                                                "GetQuality_DepartmentNewAdd",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        throw new Exception(ret.ErrMsg);
                    }
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    //视图数据显示
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    dataGridView1.Rows.Clear();
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["defect_no"].Value = dr["defect_no"].ToString();
                            dgvr.Cells["defect_name"].Value = dr["defect_name"].ToString();
                            i++;
                        }
                    }
                    totalCount = int.Parse(dic["rowCount"].ToString());
                    this.dataGridView1.ClearSelection();
                    this.dataGridView1.Columns["Operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                    if (name == "operation")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;

                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("MODIFY"))
                        {
                            string defect_no = dataGridView1.Rows[e.RowIndex].Cells["defect_no"].Value.ToString();
                            string defect_name = dataGridView1.Rows[e.RowIndex].Cells["defect_name"].Value.ToString();

                            F_QCM_Productionline_Defects_Edit add = new F_QCM_Productionline_Defects_Edit(defect_no,defect_name);
                            add.ShowDialog();
                            FormLoad();
                        }
                        else if (cell.CurrentItem.Equals("DELETE"))
                        {
                            if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                try
                                {
                                    string defect_no = dataGridView1.CurrentRow.Cells["defect_no"].Value.ToString();
                                    Dictionary<string, object> p = new Dictionary<string, object>();

                                    p.Add("defect_no", defect_no);
                                    p.Add("Operation", "Delete");
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                         "SJ_QCMAPI", "SJ_QCMAPI.Quality_DepartmentBase", "ProductionlineDefectsM_Operation", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                    if (!ret.IsSuccess)
                                    {
                                        throw new Exception(ret.ErrMsg);
                                    }
                                    else
                                    {
                                        MessageBox.Show("操作删除成功");
                                        FormLoad();
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
