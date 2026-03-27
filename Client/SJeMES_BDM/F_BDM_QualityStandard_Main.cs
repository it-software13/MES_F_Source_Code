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
    public partial class F_BDM_QualityStandard_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_QualityStandard_Main()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void lab1_Click(object sender, EventArgs e)
        {

        }

        public int JudgeType()
        {
            int type = 0;
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("gid", YQ);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "JudgeType", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    type = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(j["RetData"].ToString());
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return type;
        }

        private void F_BDM_QualityStandard_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            GetTab();
            pageControl1.BindPageEvent += GetGeneralquality_m;
            LoadPage();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        //委托一级菜单数据
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        //查询一级菜单数据
        public void GetGeneralquality_m(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("typename1", YQ);
                data.Add("typename2", FLname);

                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "GetGeneralquality_m", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
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
                        dgvr.Cells["id"].Value = dr["ID"].ToString();
                        dgvr.Cells["general_testtype_no"].Value = dr["general_testtype_no"].ToString();
                        dgvr.Cells["quality_category_no"].Value = dr["quality_category_no"].ToString();
                        dgvr.Cells["quality_category_name"].Value = dr["quality_category_name"].ToString();
                        dgvr.Cells["remarks"].Value = dr["remarks"].ToString();
                        i++;
                    }
                    GenClass.AutoSizeColumn(dataGridView1);
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
                this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
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
            FLname = this.txt_systematic_name.Text.Trim();
            LoadPage();
        }

        //页签查询
        public void GetTab()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "GetTab", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(j["RetData"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        foreach (DataRow item in dt.Rows)
                        {
                            TabPage tabPage = new TabPage();
                            this.tab__type_standard.TabPages.Add(tabPage);
                            tabPage.Text = item["general_testtype_name"].ToString();
                            tabPage.Tag = item["general_testtype_no"].ToString();
                            this.lab_null.Text = dt.Rows[0]["general_testtype_name"].ToString();
                            YQ = dt.Rows[0]["general_testtype_no"].ToString();
                        }
                    }
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

        //切换页签
        string YQ = string.Empty;
        private void tab1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = this.dataGridView1.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
            }
            int index = this.tab__type_standard.SelectedIndex;
            this.lab_null.Text = this.tab__type_standard.TabPages[index].Text;
            YQ = this.tab__type_standard.TabPages[index].Tag.ToString();
            LoadPage();
        }

        //新增分类
        private void btn5_Click(object sender, EventArgs e)
        {
            string GENERAL_TESTTYPE_NO = YQ;
            using (BDMEdit update = new BDMEdit("", GENERAL_TESTTYPE_NO))
            {
                update.ShowDialog();
                LoadPage();
            }
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        public void TypeDelete(string id)
        {
            try
            {
                if (SJeMES_Control_Library.MessageHelper.ShowWarning(this, "是否删除").ToString().ToLower() == "ok")
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", id);
                    data.Add("general_testtype_no", YQ);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "TypeDelete", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
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

        //测试
        private void btn4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0&& e.ColumnIndex > -1)
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
                        string id = this.dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                        int type = JudgeType();
                        if (type == 0)
                        {
                            F_BDM_QualityStandard_List ff = new F_BDM_QualityStandard_List(id, YQ);
                            DialogResult r2 = ff.ShowDialog();
                        }
                        else
                        {
                            F_BDM_QualityStandard_Item item = new F_BDM_QualityStandard_Item(id, YQ, "");
                            item.ShowDialog();
                        }
                    }
                    else if (cell.CurrentItem.Equals("UPDATE"))//修改
                    {
                        string id = this.dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                        string GENERAL_TESTTYPE_NO = YQ;
                        using (BDMEdit update = new BDMEdit(id, GENERAL_TESTTYPE_NO))
                        {
                            update.ShowDialog();
                            LoadPage();
                        }
                        LoadPage();
                    }
                    else if (cell.CurrentItem.Equals("DELETE"))//删除
                    {
                        string id = this.dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                        TypeDelete(id);
                        LoadPage();
                    }

                }
            }
        }
    }
}
