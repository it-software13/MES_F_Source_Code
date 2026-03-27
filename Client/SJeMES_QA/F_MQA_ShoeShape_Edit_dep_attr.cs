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
    public partial class F_MQA_ShoeShape_Edit_dep_attr : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_MQA_ShoeShape_Edit_dep_attr()
        {
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
        /// MQA管理页面查询部门属性
        /// </summary>
        /// <param name="OBJ"></param>
        /// <returns></returns>
        public void Getmqa_mag_d_dep_attr(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("da", textBox1.Text.Trim());//材料/工序
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.MQA_ShoeShape",//类名
                                            "Getmqa_mag_d_dep_attr",//方法名
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
                        dgvr.Cells["dep_attr"].Value = dr["dep_attr"].ToString();
                        dgvr.Cells["dep_attr_name"].Value = dr["dep_attr_name"].ToString();
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

        private void F_MQA_ShoeShape_Edit_dep_attr_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += Getmqa_mag_d_dep_attr;
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
                    this.Tag = dataGridView1.Rows[i].Cells["dep_attr"].Value.ToString();
                    this.Text= dataGridView1.Rows[i].Cells["dep_attr_name"].Value.ToString();
                    this.Close();
                }
                else
                    this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }
    }
}
