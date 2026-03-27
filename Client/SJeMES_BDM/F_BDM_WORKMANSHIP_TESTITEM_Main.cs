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
    public partial class F_BDM_WORKMANSHIP_TESTITEM_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public List<code_value_OBJ> JudgeList = new List<code_value_OBJ>();
        public F_BDM_WORKMANSHIP_TESTITEM_Main()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        /// <summary>
        /// 查询数据 
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("code", txt_code.Text.Trim());
                p.Add("name", txt_name.Text.Trim());
                p.Add("judge", cmb_judge.SelectedValue);
                p.Add("remark", txt_remark.Text.Trim());
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_WORKMANSHIP_TESTITEM",//类名
                                            "GetWORKMANSHIP_TESTITEM",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dgv_data.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dgv_data.Rows.Add();
                        DataGridViewRow dgvr = dgv_data.Rows[i];
                        dgvr.Cells["code"].Value = dr["INSPECTION_CODE"].ToString();
                        dgvr.Cells["name"].Value = dr["INSPECTION_NAME"].ToString();
                        dgvr.Cells["judge"].Value = dr["judgment_criteria"].ToString();
                        string judge_text = "";
                        var judgeinfo = JudgeList.FirstOrDefault(x => x.CODE == dr["judgment_criteria"].ToString());
                        if (judgeinfo != null)
                        {
                            judge_text = judgeinfo.VALUE;
                        }
                        dgvr.Cells["judge_text"].Value = judge_text;
                        dgvr.Cells["judge_type"].Value = dr["JUDGE_TYPE"].ToString();

                        string judge_type_text = "";
                        if (dr["JUDGE_TYPE"].ToString() == "1")
                        {
                            judge_type_text = "Fixed value";
                        }
                        if (dr["JUDGE_TYPE"].ToString() == "2")
                        {
                            judge_type_text = "Upper and lower limits";
                        }
                        if (dr["JUDGE_TYPE"].ToString() == "3")
                        {
                            judge_type_text = "difference";
                        }
                        dgvr.Cells["judge_type_text"].Value = judge_type_text;
                        dgvr.Cells["judge_value"].Value = dr["STANDARD_VALUE"].ToString();
                        dgvr.Cells["remark"].Value = dr["REMARKS"].ToString();
                        dgvr.Cells["ID"].Value = dr["ID"].ToString();

                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dgv_data.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
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

        public void GetJudge()
        {
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("IfSelectNull", "1");
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_BDMAPI",//类库名
                                        "SJ_BDMAPI.BDM_Inspection",//类名
                                        "GetJudge",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            JudgeList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<code_value_OBJ>>(ret.RetData);

            cmb_judge.DataSource = JudgeList;
            cmb_judge.DisplayMember = "VALUE";
            cmb_judge.ValueMember = "CODE";
        }

        private void F_BDM_WORKMANSHIP_TESTITEM_Main_Load(object sender, EventArgs e)
        {
            //只要加载一次委托 
            pageControl1.BindPageEvent += GetList;
            GetJudge();
            LoadPage();
            this.dgv_data.ClearSelection();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            F_BDM_WORKMANSHIP_TESTITEM_Edit frm = new F_BDM_WORKMANSHIP_TESTITEM_Edit(null);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (frm.flag)
            {
                LoadPage();
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> editInfo = new Dictionary<string, object>();
            foreach (DataGridViewRow item in dgv_data.Rows)
            {
                if (Convert.ToBoolean(item.Cells["select"].Value))
                {
                    editInfo.Add("id", item.Cells["ID"].Value.ToString());
                    editInfo.Add("code", item.Cells["code"].Value.ToString());
                    editInfo.Add("name", item.Cells["name"].Value.ToString());
                    editInfo.Add("judge", item.Cells["judge"].Value.ToString());
                    editInfo.Add("judge_type", item.Cells["judge_type"].Value.ToString());
                    editInfo.Add("judge_value", item.Cells["judge_value"].Value.ToString());
                    editInfo.Add("remark", item.Cells["remark"].Value.ToString());
                    break;
                }
            }
            if (!editInfo.ContainsKey("id"))
            {
                MessageBox.Show("Please tick the inspection item to be edited");
                return;
            }

            F_BDM_WORKMANSHIP_TESTITEM_Edit frm = new F_BDM_WORKMANSHIP_TESTITEM_Edit(editInfo);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            if (frm.flag)
            {
                LoadPage();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            List<string> idlist = new List<string>();
            foreach (DataGridViewRow item in dgv_data.Rows)
            {
                if (Convert.ToBoolean(item.Cells["select"].Value))
                {
                    idlist.Add(item.Cells["ID"].Value.ToString());
                }
            }
            if (idlist.Count <= 0)
            {
                MessageBox.Show("Please tick the inspection items to be deleted");
                return;
            }

            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("idlist", string.Join(",", idlist));
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.BDM_WORKMANSHIP_TESTITEM", "DeleteWORKMANSHIP_TESTITEM", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Deleted successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    LoadPage();
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

        private void btn_search_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (F_BDM_WORKMANSHIP_TESTITEM_Custom f = new F_BDM_WORKMANSHIP_TESTITEM_Custom())
            {
                f.ShowDialog();
            }
        }
    }
}
