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
    public partial class BDM_PARAM_ITEM_M_Configure : MaterialForm
    {
        public BDM_PARAM_ITEM_M_Configure()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_BDM_WORKSHOP_SECTION_Main_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetWorkshop_SectIon;
            LoadPage();

        }

        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        public void GetWorkshop_SectIon(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("workmanship_name", textBox2.Text);
                data.Add("workshop_section_name", textBox1.Text);
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_PARAM_ITEM_M",//类名
                                            "GetWorkshopConfig_SectIon",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["ID"].Value = dr["ID"].ToString();
                        dgvr.Cells["config_no"].Value = dr["config_no"].ToString();
                        dgvr.Cells["workshop_section_name"].Value = dr["workshop_section_name"].ToString();
                        dgvr.Cells["workmanship_name"].Value = dr["workmanship_name"].ToString();
                        dgvr.Cells["WORKSHOP_SECTION_NO"].Value = dr["workshop_section_no"].ToString();
                        dgvr.Cells["WORKMANSHIP_CODE"].Value = dr["workmanship_code"].ToString();
                        dgvr.Cells["remark"].Value = dr["remark"].ToString();
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

        private void Selectbtn_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            BDM_PARAM_ITEM_M_Add frm = new BDM_PARAM_ITEM_M_Add();
            frm.ShowDialog();
            LoadPage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BDM_PARAM_ITEM_M_ConfigureAdd frm = new BDM_PARAM_ITEM_M_ConfigureAdd();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            LoadPage();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    //if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "删除")
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
                    {
                        string ID = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                        //请求api的数据展示
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        //键值对传值
                        data.Add("ID", ID);
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                    "SJ_BDMAPI",//类库名
                                                    "SJ_BDMAPI.BDM_PARAM_ITEM_M",//类名
                                                    "Delete",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(data));

                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (ret.IsSuccess)
                        {
                            string msg = SJeMES_Framework.Common.UIHelper.UImsg("successfully deleted", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                        }
                        else 
                        {
                            string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        }
                        LoadPage();
                    //}else if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "关联参数项目")
                    }else if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Associated_parameter_item")
                    {
                        string config_no = dataGridView1.Rows[e.RowIndex].Cells["config_no"].Value.ToString();
                        string where = dataGridView1.Rows[e.RowIndex].Cells["WORKSHOP_SECTION_NO"].Value.ToString();
                        BDM_PARAM_ITEM_M_Configure_Union frm = new BDM_PARAM_ITEM_M_Configure_Union(where, config_no);
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.ShowDialog();
                        LoadPage();
                    }
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
