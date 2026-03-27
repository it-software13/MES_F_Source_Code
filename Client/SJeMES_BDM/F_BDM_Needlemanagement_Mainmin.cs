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

namespace SJeMES_BDM
{
    public partial class F_BDM_Needlemanagement_Mainmin : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private Dictionary<string, object> dics = new Dictionary<string, object>();
        public F_BDM_Needlemanagement_Mainmin(Dictionary<string,object> dic)
        {
            InitializeComponent();
            dics = dic;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        public void FormLoad()
        {
            lab_org_name.Text = dics["org_name"].ToString();
            lab_production_line_name.Text = dics["production_line_name"].ToString();
            lab_needle_category_name.Text = dics["needle_category_name"].ToString();
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void F_BDM_Needlemanagement_Mainmin_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetDataList;
            FormLoad();
        }
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {

            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("m_id", dics["id"].ToString());
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Needlemanagement",//类名
                                            "BDM_Needlemanagement_View_ly",//方法名
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
                        //dgvr.Cells["syaff_name"].Value = dr["STAFF_NAME"].ToString();
                        dgvr.Cells["collar_qty"].Value = dr["COLLAR_QTY"].ToString();
                        dgvr.Cells["collar_date"].Value = dr["COLLAR_DATE"].ToString();
                        dgvr.Cells["remarks"].Value = dr["REMARKS"].ToString();
                        dgvr.Cells["id"].Value = dr["ID"].ToString();
                        i++;
                    }
                }
                GenClass.AutoSizeColumn(dataGridView1);
                totalCount = int.Parse(dic["rowCount"].ToString());

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
                        if (cell.CurrentItem.Equals("delete"))
                        {
                            if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Dictionary<string, object> p = new Dictionary<string, object>();
                                p.Add("id", dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                                p.Add("opa_type", "0");//0领用，1发针，2断针

                                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                            Program.Client.APIURL,
                                                           "SJ_BDMAPI",//类库名
                                                            "SJ_BDMAPI.BDM_Needlemanagement",//类名
                                                            "BDM_Needlemanagement_PDAdelete",//方法名
                                                            Program.Client.UserToken,//token
                                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                if (!ret.IsSuccess)
                                {
                                    MessageBox.Show(ret.ErrMsg);
                                }
                                else
                                {
                                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("删除成功！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                                    FormLoad();
                                }
                            }

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

        private void btn_add_Click(object sender, EventArgs e)
        {
            using (F_BDM_NRecipientsburs add=new F_BDM_NRecipientsburs(dics))
            {
                add.ShowDialog();
                FormLoad();
            }
        }
    }
}
