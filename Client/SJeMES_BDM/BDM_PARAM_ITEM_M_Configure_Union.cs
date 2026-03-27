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
   
    public partial class BDM_PARAM_ITEM_M_Configure_Union : MaterialForm
    {
        public  string where_workshop_section_no { set; get; }
        public string config_no { set; get; }//配置编号

        public bool isDork = true;//点击返回检查是否保存

        public BDM_PARAM_ITEM_M_Configure_Union(string where,string No)
        {
            InitializeComponent();
            where_workshop_section_no = where;
            config_no = No;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void BDM_PARAM_ITEM_M_Configure_Union_Main_Load(object sender, EventArgs e)
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
                data.Add("workshop_section_no", where_workshop_section_no);//工段种类
                data.Add("config_no", config_no);
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
                        if (dr["Checked"].ToString()!="0")
                        {
                            dgvr.Cells["ckecked"].Value = true;
                         
                        }
                        dgvr.Cells["isCheck"].Value = dr["Checked"].ToString();
                        dgvr.Cells["ID"].Value = dr["ID"].ToString();
                        dgvr.Cells["param_item_no"].Value = dr["param_item_no"].ToString();
                        dgvr.Cells["param_item_name"].Value = dr["param_item_name"].ToString();
                        dgvr.Cells["workshop_section_name"].Value = dr["workshop_section_name"].ToString();
                        dgvr.Cells["check_standard"].Value = dr["check_standard"].ToString();
                        dgvr.Cells["remark"].Value = dr["remark"].ToString();
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


        private void button1_Click(object sender, EventArgs e)
        {
            if (!isDork)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("The current page has changes that have not been saved, whether to close", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                var Isckecked = SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                if (Isckecked.ToString().ToLower() == "ok")
                {
                    this.Close();
                }
            }
            else 
            {
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> Add_No_code = new List<string>();
                List<string> Delete_No_code = new List<string>();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["ckecked"].Value!=null)
                    {
                        string checkd = dataGridView1.Rows[i].Cells["ckecked"].Value.ToString();
                        if (dataGridView1.Rows[i].Cells["isCheck"].Value.ToString()=="0")
                        {
                            if (checkd.ToLower() == "true")
                            {
                                string param_item_no = dataGridView1.Rows[i].Cells["param_item_no"].Value.ToString();
                                Add_No_code.Add(param_item_no);
                            }
                        }
                        else 
                        {
                            if (checkd.ToLower() == "false")
                            {
                                string param_item_no = dataGridView1.Rows[i].Cells["param_item_no"].Value.ToString();
                                Delete_No_code.Add(param_item_no);
                            }
                        }
                    }
                }
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("config_no", config_no);//工段种类
                data.Add("param_item_no", Add_No_code);//工段种类
                data.Add("delete_param_item_no", Delete_No_code);//工段种类
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_PARAM_ITEM_M",//类名
                                            "GetWorkshopConfigUnion_Add",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    isDork = true;
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Bind successfully", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                }
                else 
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>-1)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["ckecked"].Selected)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["isCheck"].Value.ToString()=="0")
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells["ckecked"].Value != null)
                        {
                            if (dataGridView1.Rows[e.RowIndex].Cells["ckecked"].Value.ToString().ToLower() == "true")
                            {
                                dataGridView1.Rows[e.RowIndex].Cells["ckecked"].Value = false;
                                isDork = false;
                            }
                            else
                            {
                                dataGridView1.Rows[e.RowIndex].Cells["ckecked"].Value = true;
                                isDork = false;
                            }
                        }
                        else
                        {
                            dataGridView1.Rows[e.RowIndex].Cells["ckecked"].Value = true;
                            isDork = false;
                        }
                    }
                    else 
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells["ckecked"].Value.ToString().ToLower() == "true")
                        {
                            string msg = SJeMES_Framework.Common.UIHelper.UImsg("The current line is bound, whether to cancel", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                            var Isckecked = SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                            if (Isckecked.ToString().ToLower() == "ok")
                            {
                                dataGridView1.Rows[e.RowIndex].Cells["ckecked"].Value = false;
                                isDork = false;
                            }
                        }
                        else 
                        {
                            dataGridView1.Rows[e.RowIndex].Cells["ckecked"].Value = true;
                        }
                    }
                }
                
            }
        }
    }
}
