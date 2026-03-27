using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Controls.Btn;
using SJeMES_Control_Library.Controls.DataGridView;
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
    public partial class F_BDM_QualityStandard_List : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_QualityStandard_List()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_BDM_QualityStandard_List_Load(object sender, EventArgs e)
        {

            pageControl1.BindPageEvent += GetGeneralquality_d;
            LoadPage();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        string qid = string.Empty;//一级菜单编号
        string yq = string.Empty;//通用类型编号
        public F_BDM_QualityStandard_List(string id, string YQ)
        {
            InitializeComponent();
            qid = id;
            yq = YQ;
            GetTitle(id, YQ);
        }
        //分类名称查询
        string FLname = string.Empty;
        private void btn2_Click(object sender, EventArgs e)
        {

            DataTable dt = this.dataGridView1.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
            }
            FLname = this.txt1.Text.Trim();
            LoadPage();
        }

        //获取当前分类
        public void GetTitle(string qid, string yq)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("qid", qid);
                data.Add("yq", yq);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "GetTitle", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    List<string> dt = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(j["RetData"].ToString());
                    this.lab3.Text = dt[0] + "—" + dt[1];
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //委托二级菜单数据
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        //获取二级菜单
        public void GetGeneralquality_d(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("typename", FLname);
                data.Add("qid", qid);
                data.Add("general_testtype_no", yq);
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "GetGeneralquality_d", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dataGridView1.Rows.Count >= 0)
                {
                    dataGridView1.Rows.Clear();
                }
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["序号"].Value = dr["xh"].ToString();
                        dgvr.Cells["did"].Value = dr["id"].ToString();
                        dgvr.Cells["分类代号"].Value = dr["quality_category_no"].ToString();
                        dgvr.Cells["分类名称"].Value = dr["quality_category_name"].ToString();
                        dgvr.Cells["二级分类代号"].Value = dr["secondary_category_no"].ToString();
                        dgvr.Cells["二级分类名称"].Value = dr["secondary_category_name"].ToString();
                        dgvr.Cells["备注"].Value = dr["REMARKS"].ToString();
                        i++;
                    }
                    GenClass.AutoSizeColumn(dataGridView1);
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            using (BDMEditD update = new BDMEditD(Convert.ToDouble(qid), yq))
            {
                update.ShowDialog();
                LoadPage();
            }
        }

        //删除类型
        public void TypeDeleteD(string did)
        {
            try
            {
                if (SJeMES_Control_Library.MessageHelper.ShowWarning(this, "是否删除").ToString().ToLower() == "ok")
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("did", did);
                    data.Add("general_testtype_no", yq);
                    data.Add("qid", qid);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "TypeDeleteD", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                    if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("删除成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                        LoadPage();

                    }
                    else
                        throw new Exception(j["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("DETAIL"))//编辑
                    {
                        string gmName = this.lab3.Text.Trim();//通用类型和一级菜单名称的结合
                        string dName = this.dataGridView1.Rows[e.RowIndex].Cells["二级分类名称"].Value.ToString();//二级菜单名称
                        string gmdName = gmName + "-" + dName;//通用类型和一级菜单和二级菜单名称的结合
                        string did = this.dataGridView1.Rows[e.RowIndex].Cells["二级分类代号"].Value.ToString();//二级菜单代号
                        string ids = yq + "," + qid + "," + did;
                        F_BDM_QualityStandard_Item item = new F_BDM_QualityStandard_Item(gmdName, ids);
                        item.ShowDialog();
                    }
                    else if (cell.CurrentItem.Equals("UPDATE"))//修改
                    {
                        string dno = this.dataGridView1.Rows[e.RowIndex].Cells["二级分类代号"].Value.ToString();
                        string did = this.dataGridView1.Rows[e.RowIndex].Cells["did"].Value.ToString();
                        string general_testtype_no = yq;
                        using (BDMEditD update = new BDMEditD(dno, did, general_testtype_no))
                        {
                            update.ShowDialog();
                            LoadPage();
                        }
                    }
                    else if (cell.CurrentItem.Equals("DELETE"))//删除
                    {
                        string did = this.dataGridView1.Rows[e.RowIndex].Cells["did"].Value.ToString();
                        TypeDeleteD(did);
                    }

                }
            }
        }
    }
}
