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

namespace SJeMES_ZL_KanBan
{
    public partial class FrmSelectPO : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private FrmWholeLifeMain _frm;
        string _art = string.Empty;
        public FrmSelectPO(FrmWholeLifeMain frm, string ART)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            _frm = frm;
            _art = ART;
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
        List<string> listPo = new List<string>();
        /// <summary>
        /// 查询po
        /// </summary>
        /// <param name="OBJ"></param>
        /// <returns></returns>
        public void Get_PO(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("keycode", textBox1.Text.Trim());
                data.Add("art", _art);
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_KanBanAPI",//类库名
                                            "SJ_KanBanAPI.WholeLife",//类名
                                            "GetTQC_Task_Edit_PO",//方法名
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
                dataGridViewEx1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridViewEx1.Rows.Add();
                        DataGridViewRow dgvr = dataGridViewEx1.Rows[i];
                        dgvr.Cells["po"].Value = dr["mer_po"].ToString();
                        i++;
                    }
                }

                if (listPo.Count > 0)
                {
                    for (int i = 0; i < listPo.Count; i++)
                    {
                        for (int t = 0; t < dataGridViewEx1.Rows.Count; t++)
                        {
                            if (listPo[i] == dataGridViewEx1.Rows[t].Cells["po"].Value.ToString())
                            {
                                ((SJeMES_Control_Library.DataGridViewCheckBoxCellEx)dataGridViewEx1.Rows[t].Cells["xz"]).Checked = true;
                            }
                        }
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridViewEx1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void FrmSelectPO_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += Get_PO;
            LoadPage();
            this.dataGridViewEx1.ClearSelection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listPo.Count > 0)
            {
                _frm.po(listPo);
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void dataGridViewEx1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "xz")
                {
                    bool iftrue = ((SJeMES_Control_Library.DataGridViewCheckBoxCellEx)dataGridViewEx1.Rows[e.RowIndex].Cells["xz"]).Checked;
                    string po = dataGridViewEx1.Rows[e.RowIndex].Cells["po"].Value.ToString();
                    if (iftrue)
                    {
                        if (!listPo.Contains(po))
                        {
                            listPo.Add(po);
                        }
                    }
                    else
                    {
                        listPo.Remove(po);
                    }
                }
            }
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                for (int i = 0; i < dataGridViewEx1.Rows.Count; i++)
                {
                    bool iftrue = ((SJeMES_Control_Library.DataGridViewCheckBoxCellEx)dataGridViewEx1.Rows[i].Cells["xz"]).Checked;
                    string po = dataGridViewEx1.Rows[i].Cells["po"].Value.ToString();
                    if (iftrue)
                    {
                        if (!listPo.Contains(po))
                        {
                            listPo.Add(po);
                        }
                    }
                    else
                    {
                        listPo.Remove(po);
                    }
                }
            }
        }
    }
}
