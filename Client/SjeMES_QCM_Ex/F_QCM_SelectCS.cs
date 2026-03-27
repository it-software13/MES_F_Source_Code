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

namespace SjeMES_QCM_Ex
{
    public partial class F_QCM_SelectCS : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public Dictionary<string, object> selectdic = new Dictionary<string, object>();
        public F_QCM_SelectCS()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dr = dataGridView1.CurrentRow.Index;
            if (selectdic.Count <= 0)
            {
                MessageBox.Show("Please select a maker");
                return;
            }
            //送测登记更新简称
            selectdic["JC"] = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();

            this.Close();
        }


        public void getdata()
        {
            string keyword = txt_keyword.Text.Trim();
            //获取art数据
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("keyword", keyword);
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "GetCSList",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
                return;
            }
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int i = dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[1].Value = item["SUPPLIERS_CODE"].ToString();
                dataGridView1.Rows[i].Cells[2].Value = item["SUPPLIERS_NAME"].ToString();
                dataGridView1.Rows[i].Cells[3].Value = item["JC"].ToString();
                dataGridView1.Rows[i].Cells[4].Value = item["data_type"].ToString();
            }
        }

        private void F_QCM_SelectCS_Load(object sender, EventArgs e)
        {
            getdata();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[0].Value))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = false;
                    selectdic.Clear();
                }
                else
                {
                    selectdic = new Dictionary<string, object>();
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = true;
                    selectdic.Add("SUPPLIERS_CODE", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    selectdic.Add("SUPPLIERS_NAME", dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    selectdic.Add("JC", dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    selectdic.Add("DATA_TYPE", dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (e.RowIndex != i)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = false;
                    }
                }
            }
        }

        private void txt_keyword_TextChanged(object sender, EventArgs e)
        {
            getdata();
        }

        TextBox TXT_dxzl;
        string SUPPLIERS_CODE = "";
        string JC = "";
        bool update_dxzl = false;
        private void TXT_dxzl_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = TXT_dxzl.Text;
            SUPPLIERS_CODE = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
           
            JC = TXT_dxzl.Text;
            update_dxzl = true;
        }
        private void TXT_dxzl_LostFocus(object sender, EventArgs e)
        {
            TXT_dxzl.Visible = false;
            TXT_dxzl.Dispose();
           
            if (update_dxzl)
            {

                Dictionary<string, object> data1 = new Dictionary<string, object>();
                data1.Add("SUPPLIERS_CODE", SUPPLIERS_CODE);
                data1.Add("JC", JC);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                             Program.Client.APIURL,
                                             "SJ_QCMAPI",//类库名
                                             "SJ_QCMAPI.ExShose",//类名
                                             "SaveCSJc",//方法名
                                             Program.Client.UserToken,//token
                                             Newtonsoft.Json.JsonConvert.SerializeObject(data1));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    update_dxzl = false;
                    SUPPLIERS_CODE = "";
                    JC = "";
                }
            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 3)
                {
                    if (TXT_dxzl == null || TXT_dxzl.IsDisposed)
                    {
                        TXT_dxzl = new TextBox();
                        TXT_dxzl.Enabled = true;

                        Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                        TXT_dxzl.Left = rect.Left;
                        TXT_dxzl.Top = rect.Top;
                        TXT_dxzl.Width = rect.Width;
                        TXT_dxzl.Height = rect.Height;
                        TXT_dxzl.Visible = true;
                        dataGridView1.Controls.Add(TXT_dxzl);
                        if (dataGridView1.Rows[e.RowIndex].Cells[3].Value != null && !string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()))
                        {
                            TXT_dxzl.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                        }
                        TXT_dxzl.Focus();
                        TXT_dxzl.SelectionStart = TXT_dxzl.Text.Length;
                        TXT_dxzl.LostFocus += TXT_dxzl_LostFocus;
                        TXT_dxzl.TextChanged += TXT_dxzl_TextChanged;
                    }
                }
            }
        }
    }
}
