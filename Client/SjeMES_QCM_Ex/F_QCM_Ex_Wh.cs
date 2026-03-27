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

namespace SjeMES_QCM_Ex
{
    public partial class F_QCM_Ex_Wh : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Ex_Wh()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
          Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            F_QCM_EX_Wh_Edit edit = new F_QCM_EX_Wh_Edit();
            edit.StartPosition = FormStartPosition.CenterScreen;
            edit.ShowDialog();
            if (edit.bl)
            {
                FormLoad();
            }
        }

        private void dataGridViewEx1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                F_QCM_EX_Wh_Edit edit = new F_QCM_EX_Wh_Edit(dataGridViewEx1.Rows[e.RowIndex].Cells["id"].Value.ToString(), dataGridViewEx1.Rows[e.RowIndex].Cells["code"].Value.ToString(), dataGridViewEx1.Rows[e.RowIndex].Cells["name"].Value.ToString());
                edit.StartPosition = FormStartPosition.CenterScreen;
                edit.ShowDialog();
                if(!edit.bl)
                {
                    dataGridViewEx1.Rows[e.RowIndex].Cells["code"].Value = edit._code;
                    dataGridViewEx1.Rows[e.RowIndex].Cells["name"].Value = edit._name;
                }
            }
        }

        private void dataGridViewEx1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1&&e.ColumnIndex==3)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete!", "This deletion cannot be undone！！！", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                {
                    //请求api的数据展示
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("ids", dataGridViewEx1.Rows[e.RowIndex].Cells["id"].Value.ToString());
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJ_QCMAPI",//类库名
                                                "SJ_QCMAPI.ExShose",//类名
                                                "DeleteExWh",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (ret.IsSuccess)
                    {
                        MessageBox.Show("successfully deleted");
                        dataGridViewEx1.Rows.Remove(dataGridViewEx1.Rows[e.RowIndex]);
                    }
                }
            }
        }

        private void F_QCM_Ex_Wh_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetDataList;
            FormLoad();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("keyword", txt_keyword.Text.Trim());
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "GetExWhList",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridViewEx1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        int i = dataGridViewEx1.Rows.Add();
                        dataGridViewEx1.Rows[i].Cells["id"].Value = dr["ID"].ToString(); ;
                        dataGridViewEx1.Rows[i].Cells["code"].Value = dr["WAREHOUSE_CODE"].ToString();
                        dataGridViewEx1.Rows[i].Cells["name"].Value = dr["WAREHOUSE_NAME"].ToString();
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                this.dataGridViewEx1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void FormLoad()
        {
            pageControl1.PageSize = 15;
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            FormLoad();
        }
    }
}
