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
    public partial class F_BDM_SendTestFrequency_Binding : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string mid = string.Empty;//主表id
        string mvalue = string.Empty;//主表值
        string munit = string.Empty;//主表单位
        public F_BDM_SendTestFrequency_Binding()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public F_BDM_SendTestFrequency_Binding(string id, string value, string unit)
        {
            mid = id;
            mvalue = value;
            munit = unit;
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_BDM_SendTestFrequency_Binding_Load(object sender, EventArgs e)
        {
            textBox1.Text = mvalue;
            textBox2.Text = munit;
            pageControl1.BindPageEvent += WBindingType;
            WLoadPage();
            this.dataGridView1.ClearSelection();

            pageControl2.BindPageEvent += YBindingType;
            YLoadPage();
            this.dataGridView2.ClearSelection();
        }

        //委托查询
        public void WLoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        public void WBindingType(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("item_type_name", txtW.Text.Trim());
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.SendTestFrequency",//类名
                                            "WBindingType",//方法名
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
                        dgvr.Cells["item_type_no"].Value = dr["item_type_no"].ToString();
                        dgvr.Cells["item_type_name"].Value = dr["item_type_name"].ToString();
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

        //委托查询
        public void YLoadPage()
        {
            pageControl2.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl2.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl2.SetPage();
        }

        public void YBindingType(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("id", mid);
                data.Add("item_type_name", txtY.Text.Trim());
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.SendTestFrequency",//类名
                                            "YBindingType",//方法名
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
                dataGridView2.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView2.Rows.Add();
                        DataGridViewRow dgvr = dataGridView2.Rows[i];
                        dgvr.Cells["item_type_no2"].Value = dr["item_type_no"].ToString();
                        dgvr.Cells["item_type_name2"].Value = dr["item_type_name"].ToString();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView2.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name != "WeiBang") return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value != null && (bool)cell.Value)
            {
                cell.Value = false;
            }
            else
            {
                cell.Value = true;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;

            if (dataGridView2.Columns[e.ColumnIndex].Name != "YiBang") return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value != null && (bool)cell.Value)
            {
                cell.Value = false;
            }
            else
            {
                cell.Value = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = this.dataGridView1.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
            }
            WLoadPage();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable dt = this.dataGridView2.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
            }
            YLoadPage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //往左传
        private void button5_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                List<string> item_type_no_list = new List<string>();
                foreach (DataGridViewRow dgr in dataGridView1.Rows)
                {
                    string WeiBang = dgr.Cells["WeiBang"].EditedFormattedValue.ToString();
                    if (WeiBang == "True")
                    {
                        item_type_no_list.Add(dgr.Cells["item_type_no"].Value.ToString());
                    }
                }
                p.Add("mid", mid);
                p.Add("item_type_no_list", item_type_no_list);
                BindingType(p);
            }
        }

        //往右传
        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                List<string> item_type_no_list = new List<string>();
                foreach (DataGridViewRow dgr in dataGridView2.Rows)
                {
                    string WeiBang = dgr.Cells["YiBang"].EditedFormattedValue.ToString();
                    if (WeiBang == "True")
                    {
                        item_type_no_list.Add(dgr.Cells["item_type_no2"].Value.ToString());
                    }
                }
                p.Add("mid", mid);
                p.Add("delete", "delete");
                p.Add("item_type_no_list", item_type_no_list);
                BindingType(p);
            }
        }
        /// <summary>
        /// 绑定材料种类
        /// </summary>
        public void BindingType(Dictionary<string,object> data)
        {
            try
            {
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.SendTestFrequency", "BindingType", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    MessageBox.Show("Bind successfully!");
                    WLoadPage();
                    YLoadPage();
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

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_add_Click(object sender, EventArgs e)
        {

        }

        private void btn_gx_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgr in dataGridView1.Rows)
                {
                    dgr.Cells["WeiBang"].Value = true;
                }
            }
        }

        private void btn_gx2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgr in dataGridView2.Rows)
                {
                    dgr.Cells["YiBang"].Value = true;
                }
            }
        }

        private void btn_qx_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgr in dataGridView1.Rows)
                {
                    dgr.Cells["WeiBang"].Value = false;
                }
            }
        }

        private void btn_qx2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgr in dataGridView2.Rows)
                {
                    dgr.Cells["YiBang"].Value = false;
                }
            }
        }
    }
}
