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
    public partial class F_DQA_ShoeShape_Edit_workshop : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string shoe_no = string.Empty;
        public F_DQA_ShoeShape_Edit_workshop(string _shoe_no)
        {
            shoe_no = _shoe_no;
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
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

        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// DQA管理页面添加页签查询工段
        /// </summary>
        /// <param name="OBJ"></param>
        /// <returns></returns>
        public void Getworkshop_section(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("workshop_section", textBox1.Text.Trim());//材料/工序
                data.Add("shoe_no", shoe_no);//鞋型代号
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.DQA_ShoeShape",//类名
                                            "Getworkshop_section",//方法名
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
                        dgvr.Cells["workshop_section_no"].Value = dr["workshop_section_no"].ToString();
                        dgvr.Cells["workshop_section_name"].Value = dr["workshop_section_name"].ToString();
                        dgvr.Cells["data_source"].Value = dr["data_source"].ToString();
                        dgvr.Cells["id"].Value = dr["id"].ToString();
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

        private void F_DQA_ShoeShape_Edit_workshop_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += Getworkshop_section;
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
                catch (Exception ex)
                {

                    istrue = false;
                }
                if (istrue == true)
                {
                    string workshop_section_no = dataGridView1.Rows[i].Cells["workshop_section_no"].Value.ToString();
                    string workshop_section_name = dataGridView1.Rows[i].Cells["workshop_section_name"].Value.ToString();
                    string data_source = dataGridView1.Rows[i].Cells["data_source"].Value.ToString();
                    Editqa_record(workshop_section_no, workshop_section_name, data_source);
                }
                else
                    this.Close();
            }
        }

        /// <summary>
        /// DQA管理页面添加页签
        /// </summary>
        public void Editqa_record(string workshop_section_no,string workshop_section_name,string data_source)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("shoes_code", shoe_no);
                data.Add("workshop_section_no", workshop_section_no);
                data.Add("workshop_section_name", workshop_section_name);
                data.Add("data_source", data_source);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.DQA_ShoeShape", "Editdqa_mag_m", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    //this.Tag = "成功";
                    this.Tag = "success";
                    this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }
    }
}
