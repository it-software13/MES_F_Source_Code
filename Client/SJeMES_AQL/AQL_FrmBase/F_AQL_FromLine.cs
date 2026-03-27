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

namespace SJeMES_AQL
{
    public partial class F_AQL_FromLine : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public DataTable _dt;
        public string _fromline_list;

        public List<Dictionary<string, object>> selectlist = new List<Dictionary<string, object>>();

        public F_AQL_FromLine(string fromline_list)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            InitialFromLineData();
            _fromline_list = fromline_list;
        }

        public void InitialFromLineData()
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_AQLAPI",//类库名
                                        "SJ_AQLAPI.AQL_FinishedProduct_Information",//类名
                                        "GetFromLine",//方法名
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
                _dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(result["fromline_info"].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void bind_date(int indexRow = -1)
        {
            dataGridViewEx1.Rows.Clear();
            List<string> fromline_list = new List<string>();
            if (!string.IsNullOrEmpty(_fromline_list))
            {
                fromline_list = _fromline_list.Split(',').ToList();
            }

            if (_dt != null && _dt.Rows.Count > 0)
            {
                string searchFromLine = tb_search.Text;
                if (!string.IsNullOrEmpty(searchFromLine))
                {
                    var search_dt_rows = _dt.Select($@"FROM_LINE like '%{searchFromLine}%'");
                    if (search_dt_rows.Length > 0)
                    {
                        foreach (DataRow item in search_dt_rows)
                        {
                            int i = dataGridViewEx1.Rows.Add();
                            dataGridViewEx1.Rows[i].Cells[0].Value = (fromline_list.FirstOrDefault(x => x == item["FROM_LINE"].ToString()) == null) ? "False" : "True";
                            dataGridViewEx1.Rows[i].Cells[1].Value = item["FROM_LINE"].ToString();
                        }
                    }
                }
                else
                {
                    foreach (DataRow item in _dt.Rows)
                    {
                        int i = dataGridViewEx1.Rows.Add();
                        dataGridViewEx1.Rows[i].Cells[0].Value = (fromline_list.FirstOrDefault(x => x == item["FROM_LINE"].ToString()) == null) ? "False" : "True"; 
                        dataGridViewEx1.Rows[i].Cells[1].Value = item["FROM_LINE"].ToString();
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
                    List<string> fromline_list = new List<string>();
                    if (!string.IsNullOrEmpty(_fromline_list))
                    {
                        fromline_list = _fromline_list.Split(',').ToList();
                    }
                    var currCheck = dataGridViewEx1.Rows[e.RowIndex].Cells["Column1"].Value.ToString();
                    var fromline = dataGridViewEx1.Rows[e.RowIndex].Cells["Column2"].Value.ToString();
                    if (currCheck.ToLower() == "true")
                    {
                        fromline_list.Add(fromline);
                    }
                    else
                    {
                        fromline_list.Remove(fromline);
                    }

                    if (fromline_list.Count > 0)
                    {
                        fromline_list = fromline_list.Distinct().ToList();
                        _fromline_list = string.Join(",", fromline_list);
                    }
                    else
                    {
                        _fromline_list = "";
                    }
                    bind_date(e.RowIndex);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectlist.Clear();
            foreach (DataGridViewRow item in dataGridViewEx1.Rows)
            {
                if (item.Cells[0].Value.ToString().ToLower()=="true")
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("fromline", item.Cells[1].Value.ToString());
                    selectlist.Add(dic);
                }
            }
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
                List<string> fromline_list = new List<string>();
                if (!string.IsNullOrEmpty(_fromline_list))
                {
                    fromline_list = _fromline_list.Split(',').ToList();
                }
                var currCheck = dataGridViewEx1.Columns[e.ColumnIndex].HeaderCell.Value.ToString();
                if (currCheck.ToLower() == "true")
                {
                    foreach (DataGridViewRow item in dataGridViewEx1.Rows)
                    {
                        fromline_list.Add(item.Cells[1].Value.ToString());
                    }
                }
                else
                {
                    foreach (DataGridViewRow item in dataGridViewEx1.Rows)
                    {
                        fromline_list.Remove(item.Cells[1].Value.ToString());
                    }
                }
                if (fromline_list.Count > 0)
                {
                    fromline_list = fromline_list.Distinct().ToList();
                    _fromline_list = string.Join(",", fromline_list);
                }
                else
                {
                    _fromline_list = "";
                }
                bind_date();
            }
        }
    }
}
