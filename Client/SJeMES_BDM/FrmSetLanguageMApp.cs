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
    public partial class FrmSetLanguageMApp : MaterialForm
    {

        private bool CALL_API = false;//是否调用api
        private DataTable DT;
        private int TOTAL;
        private readonly MaterialSkinManager materialSkinManager;
        public FrmSetLanguageMApp()
        {
            InitializeComponent();

            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        public void LoadPage()
        {
            pageControl1.PageSize = 25;
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void BDM_ScrapGlueMag_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(uiDataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            pageControl1.BindPageEvent += GetMain_List;

            CALL_API = true;
            LoadPage();
        }

        public string GetDateListApi(int pageSize, int pageIndex)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            //data.Add("search", tb_search.Text.ToString());
            //data.Add("pageSize", pageSize);
            //data.Add("pageIndex", pageIndex);
            string retdata = WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_BDMAPI",//类库名
                                        "SJ_BDMAPI.UILAN_APP",//类名
                                        "SearchAppLanguageByCS",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));
            return retdata;
        }

        public void GetMain_List(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            if (TOTAL > 0)
                totalCount = TOTAL;
            try
            {
                if (CALL_API)
                {
                    string retdata = GetDateListApi(pageSize, pageIndex);
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    //视图数据显示
                    DT = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    TOTAL = int.Parse(dic["rowCount"].ToString());
                    totalCount = TOTAL;
                    CALL_API = false;
                }
                uiDataGridView1.Rows.Clear();
                if (DT != null && DT.Rows.Count > 0)
                {
                    DataTable dt = null;
                    string searchStr = tb_search.Text;
                    if (!string.IsNullOrEmpty(searchStr))
                    {
                        var searchDataRow = DT.Select($@"filed_code like '%{searchStr}%' or filed_name_cn like '%{searchStr}%' or filed_name_en like '%{searchStr}%' or filed_name_yn like '%{searchStr}%'");
                        if (searchDataRow.Count() > 0)
                        {
                            var searchDt = searchDataRow.CopyToDataTable();
                            totalCount = searchDt.Rows.Count;
                            dt = GetPageDataTable(searchDt, pageIndex, pageSize);
                        }
                        else
                        {
                            dt = new DataTable();
                            totalCount = 0;
                        }
                    }
                    else
                    {
                        dt = GetPageDataTable(DT, pageIndex, pageSize);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            uiDataGridView1.Rows.Add();
                            DataGridViewRow dgvr = uiDataGridView1.Rows[i];
                            dgvr.Cells["moudle_code"].Value = dr["moudle_code"].ToString();//模块代号
                            dgvr.Cells["filed_code"].Value = dr["filed_code"].ToString();//字段代号
                            dgvr.Cells["cn"].Value = dr["filed_name_cn"].ToString();
                            dgvr.Cells["en"].Value = dr["filed_name_en"].ToString();
                            dgvr.Cells["yn"].Value = dr["filed_name_yn"].ToString();
                            i++;
                        }
                    }

                    uiDataGridView1.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            CALL_API = true;
            LoadPage();
        }

        private void uiDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    switch (uiDataGridView1.Columns[e.ColumnIndex].Name)
                    {
                        case "operation":
                            FrmSetLanguageMAppEdit frmSetLanguageMAppEdit = new FrmSetLanguageMAppEdit(uiDataGridView1.CurrentRow);
                            frmSetLanguageMAppEdit.ShowDialog();
                            CALL_API = true;
                            LoadPage();
                            break;
                    }


                }
            }
            catch (Exception ex)
            {
                string msg = UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        public DataTable GetPageDataTable(DataTable dt, int currentPageIndex, int pageSize)
        {
            if (currentPageIndex == 0)
            {
                return dt;
            }

            DataTable newdt = dt.Clone();

            int rowbegin = (currentPageIndex - 1) * pageSize;//当前页的第一条数据在dt中的位置
            int rowend = currentPageIndex * pageSize;//当前页的最后一条数据在dt中的位置

            if (rowbegin >= dt.Rows.Count)
            {
                return newdt;
            }

            if (rowend > dt.Rows.Count)
            {
                rowend = dt.Rows.Count;
            }

            DataView dv = dt.DefaultView;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                newdt.ImportRow(dv[i].Row);
            }

            return newdt;
        }

    }
}
