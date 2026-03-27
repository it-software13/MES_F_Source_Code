using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SjeMES_QCM_Ex
{
    public partial class F_QCM_Ex_SelectART : MaterialForm
    {
        public string select_art = "";
        public DataTable curr_art_dt = new DataTable();
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Ex_SelectART()
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
                                        "GetArtList",//方法名
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
            curr_art_dt = dt;
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int i = dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[1].Value = item["PROD_NO"].ToString();
            }
        }

        private void F_QCM_Ex_SelectART_Load(object sender, EventArgs e)
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
                    select_art = "";
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = true;
                    select_art = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(select_art))
            {
                MessageBox.Show("Please select ART");
                return;
            }
            this.Close();
        }
    }
}
