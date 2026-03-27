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

namespace SJeMES_QA
{
    public partial class F_DQA_ShoeShape_trait_Insert_material : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        string tabid = string.Empty;
        string art_no_list = string.Empty;
        DataTable DT = null;
        public F_DQA_ShoeShape_trait_Insert_material(string _art_no_list)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            art_no_list = _art_no_list;
        }

        public F_DQA_ShoeShape_trait_Insert_material(string _tabid, string _art_no_list)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            tabid = _tabid;
            art_no_list = _art_no_list;
        }
        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// 各阶段样品记录添加页面查询材料/工序
        /// </summary>
        /// <param name="OBJ"></param>
        /// <returns></returns>
        public void Getchoice(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                if (this.DT == null)
                {
                    //请求api的数据展示
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    //键值对传值
                    data.Add("art_no_list", art_no_list);
                    data.Add("mid", tabid);//工段编号
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJ_BDMAPI",//类库名
                                                "SJ_BDMAPI.DQA_ShoeShape",//类名
                                                "GetchoiceByArt",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(data));

                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                    if (!ret.IsSuccess)
                    {
                        throw new Exception(ret.ErrMsg);
                    }

                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                    this.DT = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                }
                dataGridView1.Rows.Clear();
                if (DT.Rows.Count > 0)
                {
                    string searchStr = textBox1.Text;
                    if (!string.IsNullOrEmpty(searchStr))
                    {
                       // var searchDtRows = this.DT.Select($@"choice_no like '%{searchStr}%' or choice_name like '%{textBox1.Text}%'or position_cn like '%{textBox1.Text}%'");
                        var searchDtRows = this.DT.Select($@"choice_no like '%{searchStr}%' or choice_name like '%{textBox1.Text}%'or position_en like '%{textBox1.Text}%'");
                        if (searchDtRows.Length > 0)
                        {
                            var searchDt = GetPageDataTable(searchDtRows.CopyToDataTable(), pageIndex, pageSize);
                            int i = 0;
                            foreach (DataRow dr in searchDt.Rows)
                            {
                                dataGridView1.Rows.Add();
                                DataGridViewRow dgvr = dataGridView1.Rows[i];
                                dgvr.Cells["choice_no"].Value = dr["choice_no"].ToString();
                                dgvr.Cells["choice_name"].Value = dr["choice_name"].ToString();
                                dgvr.Cells["POSITION"].Value = dr["position"].ToString();
                                dgvr.Cells["POSITION_EN"].Value = dr["position_en"].ToString();
                                //dgvr.Cells["position"].Value = dr["position"].ToString();
                                //dgvr.Cells["position_cn"].Value = dr["position_en"].ToString();
                                i++;
                            }
                            totalCount = searchDtRows.Length;
                        }
                        else
                        {
                            totalCount = 0;
                        }
                    }
                    else
                    {
                        var dt = GetPageDataTable(DT, pageIndex, pageSize);
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["choice_no"].Value = dr["choice_no"].ToString();
                            dgvr.Cells["choice_name"].Value = dr["choice_name"].ToString();
                            dgvr.Cells["POSITION"].Value = dr["position"].ToString();
                            dgvr.Cells["POSITION_EN"].Value = dr["position_en"].ToString();
                            //dgvr.Cells["position"].Value = dr["position"].ToString();
                            //dgvr.Cells["position_cn"].Value = dr["position_en"].ToString();
                            i++;
                        }
                        totalCount = DT.Rows.Count;
                    }
                    dataGridView1.ClearSelection();
                }
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

        private void F_DQA_ShoeShape_trait_Insert_material_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += Getchoice;
            LoadPage();
            this.dataGridView1.ClearSelection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool istrue = false;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    istrue = (bool)dataGridView1.Rows[i].Cells["xz"].Value;
                }
                catch { istrue = false; }
                if (istrue == true)
                {
                    string choice_no = dataGridView1.Rows[i].Cells["choice_no"].Value.ToString();
                    string choice_name = dataGridView1.Rows[i].Cells["choice_name"].Value.ToString();
                    this.Tag = choice_no + "," + choice_name;
                    this.Close();
                }
                else
                    this.Close();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell ck = dataGridView1.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                if (i != e.RowIndex)
                {
                    ck.Value = false;
                }
                else
                {
                    ck.Value = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }
    }
}
