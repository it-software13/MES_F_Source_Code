using MaterialSkin;
using MaterialSkin.Controls;
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
    public partial class QA_RISK_DETAILS : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public DataTable _dt;
        public string _po_list;

        public List<Dictionary<string, object>> selectlist = new List<Dictionary<string, object>>();

        public QA_RISK_DETAILS(string po_list)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            InitialPoData();
            _po_list = po_list;
        }

        public void InitialPoData()
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_BDMAPI",//类库名
                                        "SJ_BDMAPI.DQA_ShoeShape",//类名
                                        "GetQaRiskDetails",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
            }
            else
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                _dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(result["art_info"].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void bind_date(int indexRow = -1)
        {
            dataGridViewEx1.Rows.Clear();
            dataGridViewEx2.Rows.Clear();
            List<string> po_list = new List<string>();
            if (!string.IsNullOrEmpty(_po_list))
            {
                po_list = _po_list.Split(',').ToList();
            }

            if (_dt != null && _dt.Rows.Count > 0)
            {
                foreach (var po in po_list)
                {
                    var search_dt_rows = _dt.Select($@"PROD_NO = '{po}'");
                    if (search_dt_rows.Length > 0)
                    {
                        foreach (DataRow item in search_dt_rows)
                        {
                            int i = dataGridViewEx2.Rows.Add();
                            dataGridViewEx2.Rows[i].Cells[0].Value = item["PROD_NO"].ToString();
                        }
                    }
                }
            }

            if (_dt != null && _dt.Rows.Count > 0)
            {
                string searchPo = tb_search.Text;
                if (!string.IsNullOrEmpty(searchPo))
                {
                    var search_dt_rows = _dt.Select($@"PROD_NO like '%{searchPo}%'");
                    if (search_dt_rows.Length > 0)
                    {
                        foreach (DataRow item in search_dt_rows)
                        {
                            int i = dataGridViewEx1.Rows.Add();
                            dataGridViewEx1.Rows[i].Cells[0].Value = (po_list.FirstOrDefault(x => x == item["PROD_NO"].ToString()) == null) ? "False" : "True";
                            dataGridViewEx1.Rows[i].Cells[1].Value = item["PROD_NO"].ToString();
                        }
                    }
                }
                else
                {
                    foreach (DataRow item in _dt.Rows)
                    {
                        int i = dataGridViewEx1.Rows.Add();
                        dataGridViewEx1.Rows[i].Cells[0].Value = (po_list.FirstOrDefault(x => x == item["PROD_NO"].ToString()) == null) ? "False" : "True"; 
                        dataGridViewEx1.Rows[i].Cells[1].Value = item["PROD_NO"].ToString();
                    }
                }

                if (indexRow != -1)
                {
                    dataGridViewEx1.FirstDisplayedScrollingRowIndex = indexRow;
                }

            }
        }

        private void F_QCM_Ex_PoOrder_Load(object sender, EventArgs e)
        {
            bind_date();
        }

        private void dataGridViewEx1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "Column1")
                {
                    List<string> po_list = new List<string>();
                    if (!string.IsNullOrEmpty(_po_list))
                    {
                        po_list = _po_list.Split(',').ToList();
                    }
                    var currCheck = dataGridViewEx1.Rows[e.RowIndex].Cells["Column1"].Value.ToString();
                    var po = dataGridViewEx1.Rows[e.RowIndex].Cells["Column2"].Value.ToString();
                    if (currCheck.ToLower() == "true")
                    {
                        po_list.Add(po);
                    }
                    else
                    {
                        po_list.Remove(po);
                    }

                    if (po_list.Count > 0)
                    {
                        po_list = po_list.Distinct().ToList();
                        _po_list = string.Join(",", po_list);
                    }
                    else
                    {
                        _po_list = "";
                    }
                    bind_date(e.RowIndex);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectlist.Clear();
            foreach (DataGridViewRow item in dataGridViewEx2.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("poorder", item.Cells[0].Value.ToString());
                selectlist.Add(dic);
            }
            //if(selectlist.Count<=0)
            //{
            //    MessageBox.Show("请选择PO订单");
            //    return;
            //}
            this.Close();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            bind_date();
        }

        private void dataGridViewEx1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "Column1")
            {
                List<string> po_list = new List<string>();
                if (!string.IsNullOrEmpty(_po_list))
                {
                    po_list = _po_list.Split(',').ToList();
                }
                var currCheck = dataGridViewEx1.Columns[e.ColumnIndex].HeaderCell.Value.ToString();
                if (currCheck.ToLower() == "true")
                {
                    foreach (DataGridViewRow item in dataGridViewEx1.Rows)
                    {
                        po_list.Add(item.Cells[1].Value.ToString());
                    }
                }
                else
                {
                    foreach (DataGridViewRow item in dataGridViewEx1.Rows)
                    {
                        po_list.Remove(item.Cells[1].Value.ToString());
                    }
                }
                if (po_list.Count > 0)
                {
                    po_list = po_list.Distinct().ToList();
                    _po_list = string.Join(",", po_list);
                }
                else
                {
                    _po_list = "";
                }
                bind_date();
            }
        }
    }
}
