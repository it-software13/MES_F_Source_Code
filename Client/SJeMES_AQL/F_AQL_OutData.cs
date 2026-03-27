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

namespace SJeMES_AQL
{
    public partial class F_AQL_OutData : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public List<Dictionary<string,object>> selectdata = new List<Dictionary<string, object>>();
        public F_AQL_OutData()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
          Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void F_AQL_OutData_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetMain_List;
            LoadPage();
        }
        public void LoadPage()
        {
            pageControl1.PageSize = 15;
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        public void GetMain_List(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string strwhere = string.Empty;
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    strwhere +=$@" and (r.prod_no like '%{textBox1.Text}%' or m.se_id like '%{textBox1.Text}%' or m.mer_po like '%{textBox1.Text}%' or l.name_t like '%{textBox1.Text}%')";
                }
                string sql = $@"
SELECT
     (select listagg(distinct f.from_line,',') from mms_finishedtrackin_list f where f.se_id=m.se_id) as 组别,
	r.prod_no,--art
	M.se_id,--制令号
	M.mer_po,--po号
	l.name_t, --鞋型
     m.SHIPCOUNTRY_name,
    E.se_qty,
     (select listagg(distinct a.shelf_no,',') from wms_stoc_location a where a.batch_no = m.se_id) as 存放位置
FROM
	BDM_SE_ORDER_MASTER M
LEFT JOIN BDM_SE_ORDER_ITEM E ON M .SE_ID = E .SE_ID
LEFT JOIN bdm_rd_prod r ON E .prod_no = r.PROD_NO
LEFT JOIN BDM_RD_STYLE l ON r.SHOE_NO = l.SHOE_NO  where 1=1 {strwhere} order by m.mer_po asc";
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("sql",sql);
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_Theinspectionplan",//类名
                                            "Outdata",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["Column1"].Value = dr["mer_po"].ToString();
                        dgvr.Cells["Column2"].Value = dr["prod_no"].ToString();
                        dgvr.Cells["Column3"].Value = dr["name_t"].ToString();
                        dgvr.Cells["Column4"].Value = dr["se_id"].ToString();

                        //dgvr.Cells["PO"].Value = dr["PO"].ToString();
                        dgvr.Cells["SHIPCOUNTRY_name"].Value = dr["SHIPCOUNTRY_name"].ToString();
                        dgvr.Cells["se_qty"].Value = dr["se_qty"].ToString();
                        dgvr.Cells["存放位置"].Value = dr["存放位置"].ToString();
                        dgvr.Cells["组别"].Value = dr["组别"].ToString();
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
        Dictionary<string, object> dic = new Dictionary<string, object>();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
              
                if (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[0].Value))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = false;
                    string mer_po = dataGridView1.Rows[e.RowIndex].Cells["Column1"].Value.ToString();
                    textBox2.Text = textBox2.Text.Replace(mer_po+"/", "");
                    selectdata.ForEach((a) =>
                    {
                        if (a.ContainsKey("mer_po"))
                        {
                            if (a["mer_po"].ToString().Equals(mer_po))
                            {
                                a.Clear();
                            }
                        }    
                    });
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = true;
                    if (!textBox2.Text.Contains(dataGridView1.Rows[e.RowIndex].Cells["Column1"].Value.ToString()))
                    {
                        dic = new Dictionary<string, object>();
                        dic.Add("mer_po", dataGridView1.Rows[e.RowIndex].Cells["Column1"].Value.ToString());//po
                        dic.Add("prod_no", dataGridView1.Rows[e.RowIndex].Cells["Column2"].Value.ToString());//art
                        dic.Add("name_t", dataGridView1.Rows[e.RowIndex].Cells["Column3"].Value.ToString());//鞋型
                        dic.Add("se_id", dataGridView1.Rows[e.RowIndex].Cells["Column4"].Value.ToString());//制令号
                        dic.Add("se_qty", dataGridView1.Rows[e.RowIndex].Cells["se_qty"].Value.ToString());//
                        dic.Add("SHIPCOUNTRY_name", dataGridView1.Rows[e.RowIndex].Cells["SHIPCOUNTRY_name"].Value.ToString());//
                        dic.Add("存放位置", dataGridView1.Rows[e.RowIndex].Cells["存放位置"].Value.ToString());//
                        dic.Add("组别", dataGridView1.Rows[e.RowIndex].Cells["组别"].Value.ToString());//

                        textBox2.Text += dataGridView1.Rows[e.RowIndex].Cells["Column1"].Value.ToString() + "/";
                        selectdata.Add(dic);
                    }

                }
            }
        }


        bool flag = true;
        private void ucBtnImg4_BtnClick(object sender, EventArgs e)
        {
            selectdata= selectdata.Where(x=>x.Count()>0).ToList();
            flag = false;
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text =string.Empty;
            dic= new Dictionary<string, object>();
            selectdata = new List<Dictionary<string, object>>();
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgr in dataGridView1.Rows)
                {
                    dgr.Cells[0].Value = false;
                }
            }
        }

        private void F_AQL_OutData_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flag)
            {
                selectdata = new List<Dictionary<string, object>>();
            }
           
        }

      
    }
}
