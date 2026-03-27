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
    public partial class F_BDM_SendTestFrequency_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_SendTestFrequency_Main()
        {
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

        public void GetData(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("data", txtdata.Text);
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.SendTestFrequency",//类名
                                            "GetSendTestFrequency",//方法名
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
                        dgvr.Cells["ID"].Value = dr["ID"].ToString();
                        dgvr.Cells["INSPECTION_FREQUENCY_VALUE"].Value = dr["INSPECTION_FREQUENCY_VALUE"].ToString();
                        dgvr.Cells["INSPECTION_FREQUENCY_TIME_UNIT"].Value = dr["ENUM_VALUE"].ToString();
                        dgvr.Cells["REMARKS"].Value = dr["REMARKS"].ToString();
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

        private void F_BDM_SendTestFrequency_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            pageControl1.BindPageEvent += GetData;
            LoadPage();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            DataTable dt = this.dataGridView1.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
            }
            LoadPage();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            using (F_BDM_SendTestFrequency_Edit update = new F_BDM_SendTestFrequency_Edit())
            {
                update.ShowDialog();
                LoadPage();
            }
        }

        //删除
        public void EditSendTestFrequency(string id)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("ID", id);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.SendTestFrequency", "DeleteSendTestFrequency", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    MessageBox.Show("successfully deleted!");
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("Binding"))//绑定
                    {
                        string id = this.dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                        string value = this.dataGridView1.Rows[e.RowIndex].Cells["INSPECTION_FREQUENCY_VALUE"].Value.ToString();
                        string unit = this.dataGridView1.Rows[e.RowIndex].Cells["INSPECTION_FREQUENCY_TIME_UNIT"].Value.ToString();
                        using (F_BDM_SendTestFrequency_Binding update = new F_BDM_SendTestFrequency_Binding(id,value,unit))
                        {
                            update.ShowDialog();
                            LoadPage();
                        }
                        LoadPage();
                    }
                    else if (cell.CurrentItem.Equals("UPDATE"))//修改
                    {
                        string id = this.dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                        using (F_BDM_SendTestFrequency_Edit update = new F_BDM_SendTestFrequency_Edit(id))
                        {
                            update.ShowDialog();
                            LoadPage();
                        }
                        LoadPage();
                    }
                    else if (cell.CurrentItem.Equals("DELETE"))//删除
                    {
                        string id = this.dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                        if (MessageBox.Show("confirm deletion? ", "This operation cannot be undone", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            EditSendTestFrequency(id);
                        }   
                    }

                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
