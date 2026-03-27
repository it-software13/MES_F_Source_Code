using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_AQL
{
    public partial class F_AQL_OutData_New : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public DataTable _dt;
        public string _PO;
        public List<Dictionary<string, object>> selectdata;
        public F_AQL_OutData_New(string PO)
        {
            InitializeComponent();
            _PO = PO;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            GetDataList();
        }

        async Task<string> AsyncFunc(Dictionary<string, object> p)
        {
            string res = "";
            await Task.Run(() =>
            {
                res = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_AQLAPI",//类库名
                                        "SJ_AQLAPI.AQL_Theinspectionplan",//类名
                                        "Outdata_New",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            });
            return res;
        }

        public async Task GetDataList(int indexRow = -1)
        {

            dataGridViewEx1.Rows.Clear();
            dataGridViewEx2.Rows.Clear();

            string strwhere = string.Empty;
            if (!string.IsNullOrWhiteSpace(tb_search.Text))
            {
                strwhere += $@" and (r.prod_no like '%{tb_search.Text}%' or m.se_id like '%{tb_search.Text}%' or m.mer_po like '%{tb_search.Text}%' or l.name_t like '%{tb_search.Text}%')";
            }
            string sql = $@"
SELECT
	(select listagg(distinct f.from_line,',') from mms_finishedtrackin_list f where f.se_id=m.se_id) as 组别,
	--listagg(distinct mfl.from_line,',') as 组别,
	MAX(r.prod_no) as ART,--art
	M.se_id as 制令号,--制令号
	MAX(M.mer_po) as PO,--po号
	MAX(l.name_t) as 鞋型, --鞋型
	MAX(m.descountry_name) as 国家,
	MAX(E.se_qty) as 数量,
	(select max(distinct a.shelf_no) from wms_stoc_location a where a.batch_no = m.se_id) as 存放位置,
-- 	(select listagg(distinct f.ORG_ID,',') from mms_finishedtrackin_list f where f.se_id=m.se_id) as 工厂代号,
	'' as 工厂代号,
	'' as 工厂
-- 	listagg(distinct mfl.ORG_ID,',') as 工厂代号,
-- 	listagg(distinct b.ORG_NAME,',') as 工厂
FROM
	BDM_SE_ORDER_MASTER M
INNER JOIN BDM_SE_ORDER_ITEM E ON E.SE_ID = M.SE_ID AND E.ORG_ID = M.ORG_ID
LEFT JOIN bdm_rd_prod r ON E .prod_no = r.PROD_NO
LEFT JOIN BDM_RD_STYLE l ON r.SHOE_NO = l.SHOE_NO  
where 1=1 {strwhere}  
GROUP BY m.mer_po,m.SE_ID,m.ORG_ID 
--order by m.mer_po asc";

//            string sql2 = $@"   
//SELECT
//	DISTINCT M.se_id 
//FROM
//	BDM_SE_ORDER_MASTER M
//LEFT JOIN BDM_SE_ORDER_ITEM E ON M .SE_ID = E .SE_ID
//LEFT JOIN bdm_rd_prod r ON E .prod_no = r.PROD_NO
//LEFT JOIN BDM_RD_STYLE l ON r.SHOE_NO = l.SHOE_NO  where 1=1 {strwhere} ";

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("sql", sql);
            //p.Add("sql2", sql2);

            Application.DoEvents();//转让控制权
            string retdata = await AsyncFunc(p);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            Application.DoEvents();//转让控制权
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //视图数据显示
            _dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());

            if (_dt.Rows.Count > 0)
            {
                int i = 0;
                var list = _PO.Split(',').ToArray();
                dataGridViewEx1.Rows.Clear();
                dataGridViewEx2.Rows.Clear();

                List<string> artist = new List<string>();
                if (!string.IsNullOrEmpty(_PO))
                {
                    artist = _PO.Split(',').ToList();
                }

                if (_dt != null && _dt.Rows.Count > 0)
                {
                    foreach (var art in artist)
                    {
                        var search_dt_rows = _dt.Select($@"PO = '{art}'");
                        if (search_dt_rows.Length > 0)
                        {
                            foreach (DataRow item in search_dt_rows)
                            {
                                Application.DoEvents();//转让控制权
                                int x = dataGridViewEx2.Rows.Add();
                                dataGridViewEx2.Rows[x].Cells["PO2"].Value = item["PO"].ToString();
                                dataGridViewEx2.Rows[x].Cells["ART2"].Value = item["ART"].ToString();
                                dataGridViewEx2.Rows[x].Cells["鞋型2"].Value = item["鞋型"].ToString();
                                dataGridViewEx2.Rows[x].Cells["制令号2"].Value = item["制令号"].ToString();
                                dataGridViewEx2.Rows[x].Cells["国家2"].Value = item["国家"].ToString();
                                dataGridViewEx2.Rows[x].Cells["数量2"].Value = item["数量"].ToString();
                                dataGridViewEx2.Rows[x].Cells["存放位置2"].Value = item["存放位置"].ToString();
                                dataGridViewEx2.Rows[x].Cells["组别2"].Value = item["组别"].ToString();
                                dataGridViewEx2.Rows[x].Cells["工厂2"].Value = item["工厂"].ToString();
                            }
                        }
                    }
                }


                if (_dt != null && _dt.Rows.Count > 0)
                {
                    
                    string search = tb_search.Text;
                    if (!string.IsNullOrEmpty(search))
                    {
                        var search_dt_rows = _dt.Select($@"PO like '%{search}%'");
                        //var search_dt_rows = _dt.Select($@"MER_PO like '%{search}%' or PROD_NO like '%{search}%' or NAME_T like '%{search}%'");
                        //var search_dt_rows = _dt.Select($@"");

                        
                        if (search_dt_rows.Length > 0)
                        {
                            int Row = 1;
                            foreach (DataRow item in search_dt_rows)
                            {
                                //if (Row > 100)
                                //    break;

                                Application.DoEvents();//转让控制权
                                int x = dataGridViewEx1.Rows.Add();
                                dataGridViewEx1.Rows[x].Cells["Column1"].Value = (artist.FirstOrDefault(c => c == item["PO"].ToString()) == null) ? "False" : "True";

                                //dataGridViewEx1.Rows[x].Cells["RN"].Value = item["RN"].ToString();
                                dataGridViewEx1.Rows[x].Cells["RN"].Value = Row;
                                dataGridViewEx1.Rows[x].Cells["PO"].Value = item["PO"].ToString();
                                dataGridViewEx1.Rows[x].Cells["ART"].Value = item["ART"].ToString();
                                dataGridViewEx1.Rows[x].Cells["鞋型"].Value = item["鞋型"].ToString();
                                dataGridViewEx1.Rows[x].Cells["制令号"].Value = item["制令号"].ToString();
                                dataGridViewEx1.Rows[x].Cells["国家"].Value = item["国家"].ToString();
                                dataGridViewEx1.Rows[x].Cells["数量"].Value = item["数量"].ToString();
                                dataGridViewEx1.Rows[x].Cells["存放位置"].Value = item["存放位置"].ToString();
                                dataGridViewEx1.Rows[x].Cells["组别"].Value = item["组别"].ToString();
                                dataGridViewEx1.Rows[x].Cells["工厂"].Value = item["工厂"].ToString();
                                Row++;

                            }
                        }
                    }
                    else
                    {
                        int Row2 = 1;
                        int Row = 1;
                        foreach (DataRow item in _dt.Rows)
                        {
                            Application.DoEvents();//转让控制权
                            if (Row2 > 1000)
                                break;
                           
                            int x = dataGridViewEx1.Rows.Add();
                            dataGridViewEx1.Rows[x].Cells["Column1"].Value = (artist.FirstOrDefault(c => c == item["PO"].ToString()) == null) ? "False" : "True";
                            //dataGridViewEx1.Rows[x].Cells["RN"].Value = item["RN"].ToString();
                            dataGridViewEx1.Rows[x].Cells["RN"].Value = Row;
                            dataGridViewEx1.Rows[x].Cells["PO"].Value = item["PO"].ToString(); ;
                            dataGridViewEx1.Rows[x].Cells["ART"].Value = item["ART"].ToString();
                            dataGridViewEx1.Rows[x].Cells["鞋型"].Value = item["鞋型"].ToString();
                            dataGridViewEx1.Rows[x].Cells["制令号"].Value = item["制令号"].ToString();
                            dataGridViewEx1.Rows[x].Cells["国家"].Value = item["国家"].ToString();
                            dataGridViewEx1.Rows[x].Cells["数量"].Value = item["数量"].ToString();
                            dataGridViewEx1.Rows[x].Cells["存放位置"].Value = item["存放位置"].ToString();
                            dataGridViewEx1.Rows[x].Cells["组别"].Value = item["组别"].ToString();
                            dataGridViewEx1.Rows[x].Cells["工厂"].Value = item["工厂"].ToString();
                            Row++;
                            Row2++;
                        }
                    }



                }
            }

            SJeMES_Framework.Common.UIHelper.LoadDgv(dataGridViewEx1);
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            //初始化数据
            //GetDataList();
            dataGridViewEx1.Rows.Clear();
            dataGridViewEx2.Rows.Clear();
            List<string> art_list = new List<string>();
            if (!string.IsNullOrEmpty(_PO))
            {
                art_list = _PO.Split(',').ToList();
            }

            if (_dt != null && _dt.Rows.Count > 0)
            {
                foreach (var art in art_list)
                {
                    var search_dt_rows = _dt.Select($@"PO = '{art}'");
                    if (search_dt_rows.Length > 0)
                    {
                        foreach (DataRow item in search_dt_rows)
                        {
                            int x = dataGridViewEx2.Rows.Add();
                            dataGridViewEx2.Rows[x].Cells["PO2"].Value = item["PO"].ToString();
                            dataGridViewEx2.Rows[x].Cells["ART2"].Value = item["ART"].ToString();
                            dataGridViewEx2.Rows[x].Cells["鞋型2"].Value = item["鞋型"].ToString();
                            dataGridViewEx2.Rows[x].Cells["制令号2"].Value = item["制令号"].ToString();
                            dataGridViewEx2.Rows[x].Cells["国家2"].Value = item["国家"].ToString();
                            dataGridViewEx2.Rows[x].Cells["数量2"].Value = item["数量"].ToString();
                            dataGridViewEx2.Rows[x].Cells["存放位置2"].Value = item["存放位置"].ToString();
                            dataGridViewEx2.Rows[x].Cells["组别2"].Value = item["组别"].ToString();
                            dataGridViewEx2.Rows[x].Cells["工厂2"].Value = item["工厂"].ToString();
                        }
                    }
                }
            }

            if (_dt != null && _dt.Rows.Count > 0)
            {
                string searchPO = tb_search.Text;
                string searchZuBie = txt_zubie.Text;
                string searchGongChang = txt_gongchang.Text;
                if (!string.IsNullOrEmpty(searchPO) || !string.IsNullOrEmpty(searchZuBie) || !string.IsNullOrEmpty(searchGongChang))
                {
                    string whereStr = "1=1";
                    if (!string.IsNullOrEmpty(searchPO))
                    {
                        List<string> searchList = searchPO.Split(new char[2] { '\r', '\n' }).ToList();
                        searchList = searchList.Where(x => !string.IsNullOrEmpty(x)).ToList();
                        whereStr += $@" and ({string.Join(" or ", searchList.Select(x => $@"PO = '{x}'"))})";
                    }
                    if (!string.IsNullOrEmpty(searchZuBie))
                    {
                        whereStr += $@" and 组别 like '%{searchZuBie}%'";
                    }
                    if (!string.IsNullOrEmpty(ORG_ID))
                    {
                        whereStr += $@" and 工厂代号 = '{ORG_ID}'";
                    }

                    var search_dt_rows = _dt.Select(whereStr);
                    if (search_dt_rows.Length > 0)
                    {
                        int Row = 1;
                        foreach (DataRow item in search_dt_rows)
                        {
                            int x = dataGridViewEx1.Rows.Add();
                            dataGridViewEx1.Rows[x].Cells["Column1"].Value = (art_list.FirstOrDefault(c => c == item["PO"].ToString()) == null) ? "False" : "True";
                            //dataGridViewEx1.Rows[x].Cells["RN"].Value = item["RN"].ToString();
                            dataGridViewEx1.Rows[x].Cells["RN"].Value = Row;
                            dataGridViewEx1.Rows[x].Cells["PO"].Value = item["PO"].ToString(); ;
                            dataGridViewEx1.Rows[x].Cells["ART"].Value = item["ART"].ToString();
                            dataGridViewEx1.Rows[x].Cells["鞋型"].Value = item["鞋型"].ToString();
                            dataGridViewEx1.Rows[x].Cells["制令号"].Value = item["制令号"].ToString();
                            dataGridViewEx1.Rows[x].Cells["国家"].Value = item["国家"].ToString();
                            dataGridViewEx1.Rows[x].Cells["数量"].Value = item["数量"].ToString();
                            dataGridViewEx1.Rows[x].Cells["存放位置"].Value = item["存放位置"].ToString();
                            dataGridViewEx1.Rows[x].Cells["组别"].Value = item["组别"].ToString();
                            dataGridViewEx1.Rows[x].Cells["工厂"].Value = item["工厂"].ToString();
                            Row++;
                        }
                    }
                }
                else
                {
                    int Row = 1;
                    foreach (DataRow item in _dt.Rows)
                    {
                        int x = dataGridViewEx1.Rows.Add();
                        dataGridViewEx1.Rows[x].Cells["Column1"].Value = (art_list.FirstOrDefault(c => c == item["PO"].ToString()) == null) ? "False" : "True";
                        dataGridViewEx1.Rows[x].Cells["RN"].Value = Row;
                        dataGridViewEx1.Rows[x].Cells["PO"].Value = item["PO"].ToString(); ;
                        dataGridViewEx1.Rows[x].Cells["ART"].Value = item["ART"].ToString();
                        dataGridViewEx1.Rows[x].Cells["鞋型"].Value = item["鞋型"].ToString();
                        dataGridViewEx1.Rows[x].Cells["制令号"].Value = item["制令号"].ToString();
                        dataGridViewEx1.Rows[x].Cells["国家"].Value = item["国家"].ToString();
                        dataGridViewEx1.Rows[x].Cells["数量"].Value = item["数量"].ToString();
                        dataGridViewEx1.Rows[x].Cells["存放位置"].Value = item["存放位置"].ToString();
                        dataGridViewEx1.Rows[x].Cells["组别"].Value = item["组别"].ToString();
                        dataGridViewEx1.Rows[x].Cells["工厂"].Value = item["工厂"].ToString();
                        Row++;
                    }
                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectdata = new List<Dictionary<string, object>>();
            for (int i = 0; i < this.dataGridViewEx2.RowCount; i++)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("PO", dataGridViewEx2.Rows[i].Cells["PO2"].Value.ToString());//po
                dic.Add("ART", dataGridViewEx2.Rows[i].Cells["ART2"].Value.ToString());//po
                dic.Add("鞋型", dataGridViewEx2.Rows[i].Cells["鞋型2"].Value.ToString());//po
                dic.Add("制令号", dataGridViewEx2.Rows[i].Cells["制令号2"].Value.ToString());//po
                dic.Add("国家", dataGridViewEx2.Rows[i].Cells["国家2"].Value.ToString());//po
                dic.Add("数量", dataGridViewEx2.Rows[i].Cells["数量2"].Value.ToString());//po
                dic.Add("存放位置", dataGridViewEx2.Rows[i].Cells["存放位置2"].Value.ToString());//po
                dic.Add("组别", dataGridViewEx2.Rows[i].Cells["组别2"].Value.ToString());//po
                dic.Add("工厂", dataGridViewEx2.Rows[i].Cells["工厂2"].Value.ToString());//po
                selectdata.Add(dic);

            }
            this.Close();

        }

        private void F_AQL_OutData_New_Load(object sender, EventArgs e)
        {
            
        }

        private void dataGridViewEx1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Thread.Sleep(200);
            if (e.RowIndex > -1)
            {

                //_artlist = "";
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "Column1")
                {
                    List<string> art_list = new List<string>();
                    if (!string.IsNullOrEmpty(_PO))
                    {
                        art_list = _PO.Split(',').ToList();
                    }
                    var currCheck = dataGridViewEx1.Rows[e.RowIndex].Cells["Column1"].Value.ToString();
                    var art = dataGridViewEx1.Rows[e.RowIndex].Cells["PO"].Value.ToString();
                    if (currCheck.ToLower() == "true")
                    {
                        art_list.Add(art);
                    }
                    else
                    {
                        art_list.Remove(art);
                    }

                    if (art_list.Count > 0)
                    {
                        art_list = art_list.Distinct().ToList();
                        _PO = string.Join(",", art_list);
                    }
                    else
                    {
                        _PO = "";
                    }
                    updatedata();

                }
            }
        }
        private void updatedata()
        {
            dataGridViewEx2.Rows.Clear();

            if (_dt != null && _dt.Rows.Count > 0)
            {
                var list = _PO.Split(',');
                foreach (var art in list)
                {
                    var search_dt_rows = _dt.Select($@"PO = '{art}'");
                    if (search_dt_rows.Length > 0)
                    {
                        foreach (DataRow item in search_dt_rows)
                        {
                            int x = dataGridViewEx2.Rows.Add();
                            dataGridViewEx2.Rows[x].Cells["PO2"].Value = item["PO"].ToString();
                            dataGridViewEx2.Rows[x].Cells["ART2"].Value = item["ART"].ToString();
                            dataGridViewEx2.Rows[x].Cells["鞋型2"].Value = item["鞋型"].ToString();
                            dataGridViewEx2.Rows[x].Cells["制令号2"].Value = item["制令号"].ToString();
                            dataGridViewEx2.Rows[x].Cells["国家2"].Value = item["国家"].ToString();
                            dataGridViewEx2.Rows[x].Cells["数量2"].Value = item["数量"].ToString();
                            dataGridViewEx2.Rows[x].Cells["存放位置2"].Value = item["存放位置"].ToString();
                            dataGridViewEx2.Rows[x].Cells["组别2"].Value = item["组别"].ToString();
                            dataGridViewEx2.Rows[x].Cells["工厂2"].Value = item["工厂"].ToString();
                        }
                    }
                }

            }
            //SJeMES_Framework.Common.UIHelper.LoadDgv(dataGridViewEx2);
        }

        private void dataGridViewEx1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "Column1")
            {
                List<string> art_list = new List<string>();
                if (!string.IsNullOrEmpty(_PO))
                {
                    art_list = _PO.Split(',').ToList();
                }
                var currCheck = dataGridViewEx1.Columns[e.ColumnIndex].HeaderCell.Value.ToString();
                if (currCheck.ToLower() == "true")
                {
                    foreach (DataGridViewRow item in dataGridViewEx1.Rows)
                    {
                        art_list.Add(item.Cells["PO"].Value.ToString());
                    }
                }
                else
                {
                    foreach (DataGridViewRow item in dataGridViewEx1.Rows)
                    {
                        art_list.Remove(item.Cells["PO"].Value.ToString());
                    }
                }
                if (art_list.Count > 0)
                {
                    art_list = art_list.Distinct().ToList();
                    _PO = string.Join(",", art_list);
                }
                else
                {
                    _PO = "";
                }
                updatedata();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string ORG_ID;
        public string ORG_Name;
        private void txt_gongchang_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txt_gongchang_DoubleClick(object sender, EventArgs e)
        {
            string sql = string.Empty;
            sql = $@"
SELECT
	ORG_CODE 仓库代号,
	ORG_NAME 仓库名称
FROM
	BASE001M 
";
            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_gongchang.Text = frmData.RetData.Rows[0]["仓库名称"].ToString();
                ORG_ID = frmData.RetData.Rows[0]["仓库代号"].ToString();
                ORG_Name = frmData.RetData.Rows[0]["仓库名称"].ToString();
            }
        }
    }
}
