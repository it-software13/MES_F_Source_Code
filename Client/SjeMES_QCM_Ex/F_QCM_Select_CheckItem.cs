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
    public partial class F_QCM_Select_CheckItem : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        public Dictionary<string, object> selectdic = new Dictionary<string, object>();
        public string _inspection_type ="";
        public F_QCM_Select_CheckItem(string inspection_type)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            _inspection_type = inspection_type;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectdic.Count<=0)
            {
                MessageBox.Show("请选择检验项");
                return;
            }
            this.Close();
        }

        public void getdata()
        {
            string keyword = txt_keyword.Text.Trim();
            //获取art数据
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("keyword", keyword);
            data.Add("type", _inspection_type);
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "GetChectItemByType",//方法名
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
                dataGridView1.Rows[i].Cells["CODE"].Value = item["CODE"].ToString();
                dataGridView1.Rows[i].Cells["NAME"].Value = item["NAME"].ToString(); 
                dataGridView1.Rows[i].Cells["PDBZ_Code"].Value = item["PDBZ"].ToString();
                switch (item["PDBZ"].ToString())
                {
                    case ENUM_JUDGMENT_CRITERIA_CODE.JUDGMENT_CODE_0:
                        dataGridView1.Rows[i].Cells["PDBZ"].Value = ENUM_JUDGMENT_CRITERIA.JUDGMENT_0;
                        break;
                    case ENUM_JUDGMENT_CRITERIA_CODE.JUDGMENT_CODE_1:
                        dataGridView1.Rows[i].Cells["PDBZ"].Value = ENUM_JUDGMENT_CRITERIA.JUDGMENT_1;
                        break;
                    case ENUM_JUDGMENT_CRITERIA_CODE.JUDGMENT_CODE_2:
                        dataGridView1.Rows[i].Cells["PDBZ"].Value = ENUM_JUDGMENT_CRITERIA.JUDGMENT_2;
                        break;
                    case ENUM_JUDGMENT_CRITERIA_CODE.JUDGMENT_CODE_3:
                        dataGridView1.Rows[i].Cells["PDBZ"].Value = ENUM_JUDGMENT_CRITERIA.JUDGMENT_3;
                        break;
                }
               ;
                dataGridView1.Rows[i].Cells["JYBZ"].Value = item["JYBZ"].ToString();
            }
        }

        private void F_QCM_Select_CheckItem_Load(object sender, EventArgs e)
        {
            getdata();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex > -1)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[0].Value))
                {
                    selectdic= new Dictionary<string, object>();
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = false;
                    selectdic.Clear();
                }
                else
                {
                    selectdic=new Dictionary<string, object>();
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = true;
                    selectdic.Add("code", dataGridView1.Rows[e.RowIndex].Cells["CODE"].Value.ToString());
                    selectdic.Add("name", dataGridView1.Rows[e.RowIndex].Cells["NAME"].Value.ToString());
                    selectdic.Add("pdbz", dataGridView1.Rows[e.RowIndex].Cells["PDBZ_Code"].Value.ToString());
                    selectdic.Add("jybz", dataGridView1.Rows[e.RowIndex].Cells["JYBZ"].Value.ToString());
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
    }
}
