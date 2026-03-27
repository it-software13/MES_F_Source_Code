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
    public partial class F_BDM_WORKSHOP_SECTION_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_WORKSHOP_SECTION_Main()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_BDM_WORKSHOP_SECTION_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            pageControl1.BindPageEvent += GetWorkshop_SectIon;
            LoadPage();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;

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
                data.Add("workshop_section_no", textBox1.Text);//编号
                data.Add("workshop_section_name", textBox2.Text);//名称
                data.Add("remarks", textBox3.Text);//备注
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Workshop_SectIon",//类名
                                            "GetWorkshop_SectIon",//方法名
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
                        dgvr.Cells["mid"].Value = dr["id"].ToString();
                        dgvr.Cells["sorting"].Value = int.Parse(dr["sorting"].ToString());
                        dgvr.Cells["data_source"].Value = dr["data_source"].ToString();
                        dgvr.Cells["inspection_type"].Value = dr["inspection_type"].ToString();
                        dgvr.Cells["enum_value_data_source"].Value = dr["enum_value_data_source"].ToString();
                        dgvr.Cells["enum_value_inspection_type"].Value = dr["enum_value_inspection_type"].ToString();
                        dgvr.Cells["workshop_section_no"].Value = dr["workshop_section_no"].ToString();
                        dgvr.Cells["workshop_section_name"].Value = dr["workshop_section_name"].ToString();
                        dgvr.Cells["product_category"].Value = dr["product_category"].ToString();
                        dgvr.Cells["remarks"].Value = dr["remarks"].ToString();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
                this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 工段创建删除
        /// </summary>
        public void EditWorkshop_SectIon(string mid)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("mid", mid);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.BDM_Workshop_SectIon", "DeleteWorkshop_SectIon", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Delete success!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
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

        /// <summary>
        /// 工段创建排序修改
        /// </summary>
        public void UpdateGD(string mid, string sorting)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("mid", mid);
                data.Add("sorting", sorting);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.BDM_Workshop_SectIon", "UpdateGD", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Modify successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                    textBox4.Visible = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = this.dataGridView1.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
            }
            LoadPage();
        }

        string mmid = string.Empty;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "sorting") // combobox显示条件 
                {
                    mmid = dataGridView1.Rows[e.RowIndex].Cells["mid"].Value.ToString();
                    string sorting = dataGridView1.CurrentRow.Cells["sorting"].Value.ToString();
                    textBox4.Text = sorting; //对combobox赋值

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox4.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox4.Visible = true;
                }
                else
                    textBox4.Visible = false;
            }
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    else if (cell.CurrentItem.Equals("UPDATE"))//修改
                    {
                        string id = this.dataGridView1.Rows[e.RowIndex].Cells["mid"].Value.ToString();
                        using (F_BDM_WORKSHOP_SECTION_Edit update = new F_BDM_WORKSHOP_SECTION_Edit(id))
                        {
                            update.ShowDialog();
                            LoadPage();
                        }
                        LoadPage();
                    }
                    else if (cell.CurrentItem.Equals("DELETE"))//删除
                    {
                        string id = this.dataGridView1.Rows[e.RowIndex].Cells["mid"].Value.ToString();
                        EditWorkshop_SectIon(id);

                    }

                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (F_BDM_WORKSHOP_SECTION_Edit update = new F_BDM_WORKSHOP_SECTION_Edit())
            {
                update.ShowDialog();
            }
            LoadPage();
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridView1.CurrentCell.Value = textBox4.Text.ToString();
                UpdateGD(mmid, textBox4.Text.ToString());
            }
        }
    }
}
