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
    public partial class F_MQA_ShoeShape_Edit_inspection : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string tabid = string.Empty;
        string workshop_section_no = string.Empty;
        private F_MQA_ShoeShape_Edit fds;
        public F_MQA_ShoeShape_Edit_inspection()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public F_MQA_ShoeShape_Edit_inspection(string _tabid, F_MQA_ShoeShape_Edit _fds)
        {
            tabid = _tabid;
            fds = _fds;
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        public F_MQA_ShoeShape_Edit_inspection(string _tabid, F_MQA_ShoeShape_Edit _fds,string _workshop_section_no)
        {
            tabid = _tabid;
            fds = _fds;
            workshop_section_no = _workshop_section_no;
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// MQA管理页面添加时查询检测项
        /// </summary>
        /// <param name="OBJ"></param>
        /// <returns></returns>
        public void Getinspection(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("mno", tabid);//工段ID
                data.Add("workshop_section_no", workshop_section_no);//工段
                data.Add("KeyCode", textBox1.Text.Trim());//条件
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.MQA_ShoeShape",//类名
                                            "Getinspection",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

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
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["inspection_code"].Value = dr["inspection_code"].ToString();
                        dgvr.Cells["inspection_name"].Value = dr["inspection_name"].ToString();
                        dgvr.Cells["inspection_type"].Value = dr["inspection_type"].ToString();
                        dgvr.Cells["judgment_criteria"].Value = dr["judgment_criteria"].ToString();
                        dgvr.Cells["judgment_criteria_name"].Value = dr["judgment_criteria_name"].ToString();
                        dgvr.Cells["standard_value"].Value = dr["standard_value"].ToString();
                        if (dr["qc_type"].ToString() == "1")
                            dgvr.Cells["qc_type"].Value = "TQC";
                        else if (dr["qc_type"].ToString() == "2")
                            dgvr.Cells["qc_type"].Value = "RQC";
                        else
                            dgvr.Cells["qc_type"].Value = "-";
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            bool istrue = false;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    istrue = (bool)dataGridView1.Rows[i].Cells["xz"].Value;
                }
                catch { istrue = false;  }
                if (istrue == true)
                {
                    string inspection_code = dataGridView1.Rows[i].Cells["inspection_code"].Value.ToString();
                    string inspection_name = dataGridView1.Rows[i].Cells["inspection_name"].Value.ToString();
                    string inspection_type = dataGridView1.Rows[i].Cells["inspection_type"].Value.ToString();
                    string judgment_criteria = dataGridView1.Rows[i].Cells["judgment_criteria"].Value.ToString();
                    string judgment_criteria_name = dataGridView1.Rows[i].Cells["judgment_criteria_name"].Value.ToString();
                    fds.Edit_inspection(inspection_code, inspection_name, judgment_criteria, inspection_type, judgment_criteria_name);
                    this.Close();
                }
                else
                    this.Close();
            }
        }

        private void F_MQA_ShoeShape_Edit_inspection_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += Getinspection;
            LoadPage();
            this.dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }
    }
}
