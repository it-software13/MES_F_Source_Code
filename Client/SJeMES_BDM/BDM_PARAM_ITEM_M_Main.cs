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
    public partial class BDM_PARAM_ITEM_M_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public BDM_PARAM_ITEM_M_Main()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
       Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_BDM_WORKSHOP_SECTION_Main_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetWorkshop_SectIon;
            comboBox1.Items.Add("11");
            LoadPage();
            GetWorkshop();

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
                data.Add("param_item_no", textBox1.Text);//编号
                data.Add("param_item_name", textBox2.Text);//名称
                data.Add("check_standard", textBox3.Text);//标准
                data.Add("remarks", textBox4.Text);//备注
                data.Add("workshop_section_name", comboBox1.Text);//工段种类
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_PARAM_ITEM_M",//类名
                                            "GetWorkshop_SectIon",//方法名
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
                        dgvr.Cells["param_item_no"].Value = dr["param_item_no"].ToString();
                        dgvr.Cells["param_item_name"].Value = dr["param_item_name"].ToString();
                        dgvr.Cells["workshop_section_no"].Value = dr["workshop_section_no"].ToString();
                        dgvr.Cells["workshop_section_name"].Value = dr["workshop_section_name"].ToString();
                        dgvr.Cells["check_standard"].Value = dr["check_standard"].ToString();
                        dgvr.Cells["remark"].Value = dr["remark"].ToString();
                        dgvr.Cells["judgment_criteria_code"].Value = dr["judgment_criteria_code"].ToString();
                        dgvr.Cells["judgment_criteria"].Value = dr["judgment_criteria"].ToString();//在接口处获取枚举名称，不做下拉
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
        private void GetWorkshop()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_PARAM_ITEM_M",//类名
                                            "GetWorkshop",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
              
                if (dt.Rows.Count>0)
                {
                    DataRow dataRow = dt.NewRow();
                    dt.Rows.InsertAt(dataRow,0);
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "WORKSHOP_SECTION_NAME";
                    comboBox1.ValueMember = "WORKSHOP_SECTION_NO";
                    comboBox1.SelectedIndex = -1;
                 
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.ToString(), Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BDM_PARAM_ITEM_M_Configure from = new BDM_PARAM_ITEM_M_Configure();
            from.StartPosition = FormStartPosition.CenterParent;
            from.ShowDialog();
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
                        string param_item_no = dataGridView1.Rows[e.RowIndex].Cells["param_item_no"].Value.ToString();
                        //请求api的数据展示
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        //键值对传值
                        data.Add("param_item_no", param_item_no);
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                    "SJ_BDMAPI",//类库名
                                                    "SJ_BDMAPI.BDM_PARAM_ITEM_M",//类名
                                                    "DeleteParamItem",//方法名
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
                    }
                    //else if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "编辑")
                    else if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Edit")
                    {
                        DataGridViewRow currRow = dataGridView1.Rows[e.RowIndex];
                        BDM_PARAM_ITEM_M_Add frm = new BDM_PARAM_ITEM_M_Add(currRow);
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
