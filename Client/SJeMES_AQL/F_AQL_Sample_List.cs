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

namespace SJeMES_AQL
{
    public partial class F_AQL_Sample_List : MaterialForm
    {
        private string ART_NO;
        private DataTable BOM_DT;

        public F_AQL_Sample_List(string art_no)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            this.StartPosition = FormStartPosition.CenterScreen;
            dataGridViewEx1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewEx1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            ART_NO = art_no;
            pageControl1.BindPageEvent += GetData;
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

        public void GetData(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                if (BOM_DT == null)
                {
                    Dictionary<string, object> p1 = new Dictionary<string, object>();
                    p1.Add("art_no", ART_NO);
                    string retdata1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_Checkthedata1",//类名
                                            "GetAQLSampleList",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p1));
                    ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata1);
                    if (!ret1.IsSuccess)
                    {
                        throw new Exception(ret1.ErrMsg);
                    }
                    BOM_DT = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret1.RetData);
                }

                //视图数据显示
                var dt = GetPageDataTable(BOM_DT, pageIndex, pageSize);
                dataGridViewEx1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridViewEx1.Rows.Add();
                        DataGridViewRow dgvr = dataGridViewEx1.Rows[i];
                        dgvr.Cells["ART"].Value = dr["ART_NO"].ToString();
                        dgvr.Cells["STAGE"].Value = dr["STAGE"].ToString();
                        dgvr.Cells["ITEM_NO"].Value = dr["ITEM_NO"].ToString();
                        dgvr.Cells["POSITION"].Value = dr["POSITION_CN"].ToString() + dr["POSITION_EN"].ToString();
                        dgvr.Cells["NAME_CN"].Value = dr["NAME_CN"].ToString();
                        dgvr.Cells["NAME_EN"].Value = dr["NAME_EN"].ToString();
                        dgvr.Cells["process_desc"].Value = dr["process_desc"].ToString();
                        dgvr.Cells["remark"].Value = dr["remark"].ToString();
                        i++;
                    }
                }
                totalCount = BOM_DT.Rows.Count;
                dataGridViewEx1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
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
