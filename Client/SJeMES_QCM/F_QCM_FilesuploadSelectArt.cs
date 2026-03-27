using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SJeMES_QCM
{
    public partial class F_QCM_FilesuploadSelectArt : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        public string _sql;
        public string _ART;
        //public string _artlist;
        public DataTable _dt;
        public DateTime time;
        public string record;
        public List<Dictionary<string, object>> selectlist = new List<Dictionary<string, object>>();
        public F_QCM_FilesuploadSelectArt()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        public F_QCM_FilesuploadSelectArt(string ART,string sql)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            _sql = sql;
            _ART = ART;
        }

        private void F_QCM_FilesuploadSelectArt_Load(object sender, EventArgs e)
        {
            GetDataList();
        }


        public void GetDataList(int indexRow = -1)
        {

            dataGridViewEx1.Rows.Clear();
            dataGridViewEx2.Rows.Clear();
            //List<string> art_list = new List<string>();


            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("keyword", tb_search.Text);
            p.Add("sql", _sql);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.FilesuploadBase",//类名
                                        "GetData",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
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
                var list = _ART.Split(',').ToArray();
                dataGridViewEx1.Rows.Clear();
                dataGridViewEx2.Rows.Clear();

                List<string> artist = new List<string>();
                if (!string.IsNullOrEmpty(_ART))
                {
                    artist = _ART.Split(',').ToList();
                }

                if (_dt != null && _dt.Rows.Count > 0)
                {
                    foreach (var art in artist)
                    {
                        var search_dt_rows = _dt.Select($@"ART = '{art}'");
                        if (search_dt_rows.Length > 0)
                        {
                            foreach (DataRow item in search_dt_rows)
                            {
                                int x = dataGridViewEx2.Rows.Add();
                                dataGridViewEx2.Rows[x].Cells[0].Value = item["鞋型"].ToString();
                                //dataGridViewEx2.Rows[x].Cells[0].Value = item["Shoe_type"].ToString();
                                dataGridViewEx2.Rows[x].Cells[1].Value = item["ART"].ToString();
                            }
                        }
                    }
                }


                if (_dt != null && _dt.Rows.Count > 0)
                {
                    string search = tb_search.Text;
                    if (!string.IsNullOrEmpty(search))
                    {
                        var search_dt_rows = _dt.Select($@"ART like '%{search}%'");
                        if (search_dt_rows.Length > 0)
                        {
                            foreach (DataRow item in search_dt_rows)
                            {
                                int x = dataGridViewEx1.Rows.Add();
                                dataGridViewEx1.Rows[x].Cells[0].Value = (artist.FirstOrDefault(c => c == item["ART"].ToString()) == null) ? "False" : "True";
                                dataGridViewEx1.Rows[x].Cells[1].Value = item["RN"].ToString();
                                dataGridViewEx1.Rows[x].Cells[2].Value = item["鞋型"].ToString();
                                //dataGridViewEx1.Rows[x].Cells[2].Value = item["Shoe_type"].ToString();
                                dataGridViewEx1.Rows[x].Cells[3].Value = item["ART"].ToString();
                            }
                        }
                    }
                    else
                    {
                        foreach (DataRow item in _dt.Rows)
                        {
                            int x = dataGridViewEx1.Rows.Add();
                            dataGridViewEx1.Rows[x].Cells[0].Value = (artist.FirstOrDefault(c => c == item["ART"].ToString()) == null) ? "False" : "True";
                            dataGridViewEx1.Rows[x].Cells[1].Value = item["RN"].ToString();
                            dataGridViewEx1.Rows[x].Cells[2].Value = item["鞋型"].ToString();
                            //dataGridViewEx1.Rows[x].Cells[2].Value = item["Shoe_type"].ToString();
                            dataGridViewEx1.Rows[x].Cells[3].Value = item["ART"].ToString();
                        }
                    }

                    /*
                    if (list.Length > 0)
                {
                    //左侧
                    foreach (DataRow dr in _dt.Rows)
                    {
                        dataGridViewEx1.Rows.Add();
                        DataGridViewRow dgvr = dataGridViewEx1.Rows[i];
                        dgvr.Cells["行号"].Value = dr["RN"].ToString();
                        dgvr.Cells["鞋型"].Value = dr["鞋型"].ToString();
                        dgvr.Cells["ART"].Value = dr["ART"].ToString();

                        if (list.Contains(dr["ART"].ToString()))
                        {
                            dgvr.Cells["Column1"].Value = "true";
                        }
                        i++;
                    }
                   

                    //右侧
                    string search = tb_search.Text;
                    if (!string.IsNullOrEmpty(search))
                    {

                        //updatedata();
                    }
                    else
                    {
                        var search_dt_rows = _dt.Select($@"ART in({string.Join(",", list.Select(x => $"'{x}'"))})");
                        if (search_dt_rows.Length > 0)
                        {
                            foreach (DataRow item in search_dt_rows)
                            {
                                int x = dataGridViewEx2.Rows.Add();
                                //dataGridViewEx1.Rows[i].Cells[0].Value =  ? "False" : "True";
                                dataGridViewEx2.Rows[x].Cells[0].Value = item["鞋型"].ToString();
                                dataGridViewEx2.Rows[x].Cells[1].Value = item["ART"].ToString();
                            }
                        }
                    }



                    if (indexRow != -1)
                    {
                        dataGridViewEx1.FirstDisplayedScrollingRowIndex = indexRow;
                    }

                }

                    */

                }
            }
            #region old
            //if (!string.IsNullOrEmpty(_artlist))
            //{
            //    //art_list = _artlist.Split(',').ToList();


            //    //右侧dgv
            //    foreach (var artitem in art_list)
            //    {

            //        //int i = dataGridViewEx2.Rows.Add();
            //        //dataGridViewEx2.Rows[i].Cells[0].Value = artitem.ToString();
            //        //dataGridViewEx2.Rows[i].Cells[1].Value = artitem.ToString();

            //    }

            //    //左侧dgv
            //    foreach (var artitem2 in art_list)
            //    {
            //        //int i = dataGridViewEx1.Rows.Add();
            //        //dataGridViewEx1.Rows[i].Cells[0].Value = (po_list.FirstOrDefault(x => x == item["MER_PO"].ToString()) == null) ? "False" : "True";
            //        //dataGridViewEx1.Rows[i].Cells[1].Value = artitem2;
            //        //dataGridViewEx1.Rows[i].Cells[2].Value = item["SE_QTY"].ToString();
            //    }


            //}
            ////if (indexRow != -1)
            ////{
            ////    dataGridViewEx1.FirstDisplayedScrollingRowIndex = indexRow;
            ////}

            #endregion

        }

        private void dataGridViewEx1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Thread.Sleep(200);
            if (e.RowIndex > -1)
            {
                if (!string.IsNullOrEmpty(record))
                {
                    //decimal temp = 0.5M;
                    //decimal second = dateDiff(time, DateTime.Now);
                    //if(second< temp)
                    //{
                    //    dataGridViewEx1.Rows[e.RowIndex].Cells[0].Value = "False";
                    //    //dataGridViewEx1.Rows[e.RowIndex].Cells["Column1"].Value.ToString() = "False";
                    //    MessageBox.Show("系统繁忙！");
                    //    return;
                    //}
                }

                //_artlist = "";
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "Column1")
                {
                    List<string> art_list = new List<string>();
                    if (!string.IsNullOrEmpty(_ART))
                    {
                        art_list = _ART.Split(',').ToList();
                    }
                    var currCheck = dataGridViewEx1.Rows[e.RowIndex].Cells["Column1"].Value.ToString();
                    var art = dataGridViewEx1.Rows[e.RowIndex].Cells["ART"].Value.ToString();
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
                        _ART = string.Join(",", art_list);
                    }
                    else
                    {
                        _ART = "";
                    }
                    updatedata();
                    time = DateTime.Now;
                    record = DateTime.Now.ToString();
                }
            }
        }

        private void updatedata()
        {
            dataGridViewEx2.Rows.Clear();

            if (_dt != null && _dt.Rows.Count > 0)
            {
                var list = _ART.Split(',');
                foreach (var art in list)
                {
                    var search_dt_rows = _dt.Select($@"ART = '{art}'");
                    if (search_dt_rows.Length > 0)
                    {
                        foreach (DataRow item in search_dt_rows)
                        {
                            int i = dataGridViewEx2.Rows.Add();
                            //dataGridViewEx2.Rows[i].Cells[0].Value = item["RN"].ToString();
                           // dataGridViewEx2.Rows[i].Cells[0].Value = item["Shoe_type"].ToString();
                            dataGridViewEx2.Rows[i].Cells[0].Value = item["鞋型"].ToString();
                            dataGridViewEx2.Rows[i].Cells[1].Value = item["ART"].ToString();
                        }
                    }
                }
                
            }

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            dataGridViewEx1.Rows.Clear();
            dataGridViewEx2.Rows.Clear();
            List<string> art_list = new List<string>();
            if (!string.IsNullOrEmpty(_ART))
            {
                art_list = _ART.Split(',').ToList();
            }

            if (_dt != null && _dt.Rows.Count > 0)
            {
                foreach (var art in art_list)
                {
                    var search_dt_rows = _dt.Select($@"ART = '{art}'");
                    if (search_dt_rows.Length > 0)
                    {
                        foreach (DataRow item in search_dt_rows)
                        {
                            int i = dataGridViewEx2.Rows.Add();
                            dataGridViewEx2.Rows[i].Cells[0].Value = item["鞋型"].ToString();
                            //dataGridViewEx2.Rows[i].Cells[0].Value = item["Shoe_type"].ToString();
                            dataGridViewEx2.Rows[i].Cells[1].Value = item["ART"].ToString();
                        }
                    }
                }
            }

            if (_dt != null && _dt.Rows.Count > 0)
            {
                string search = tb_search.Text;
                if (!string.IsNullOrEmpty(search))
                {
                    var search_dt_rows = _dt.Select($@"ART like '%{search}%'");
                    if (search_dt_rows.Length > 0)
                    {
                        foreach (DataRow item in search_dt_rows)
                        {
                            int i = dataGridViewEx1.Rows.Add();
                            dataGridViewEx1.Rows[i].Cells[0].Value = (art_list.FirstOrDefault(x => x == item["ART"].ToString()) == null) ? "False" : "True";
                            dataGridViewEx1.Rows[i].Cells[1].Value = item["RN"].ToString();
                            dataGridViewEx1.Rows[i].Cells[2].Value = item["鞋型"].ToString();
                            //dataGridViewEx1.Rows[i].Cells[2].Value = item["Shoe_type"].ToString();
                            dataGridViewEx1.Rows[i].Cells[3].Value = item["ART"].ToString();
                        }
                    }
                }
                else
                {
                    foreach (DataRow item in _dt.Rows)
                    {
                        int i = dataGridViewEx1.Rows.Add();
                        dataGridViewEx1.Rows[i].Cells[0].Value = (art_list.FirstOrDefault(x => x == item["ART"].ToString()) == null) ? "False" : "True";
                        dataGridViewEx1.Rows[i].Cells[1].Value = item["RN"].ToString();
                        dataGridViewEx1.Rows[i].Cells[2].Value = item["鞋型"].ToString();
                        //dataGridViewEx1.Rows[i].Cells[2].Value = item["Shoe_type"].ToString();
                        dataGridViewEx1.Rows[i].Cells[3].Value = item["ART"].ToString();
                    }
                }


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectlist.Clear();
            foreach (DataGridViewRow item in dataGridViewEx2.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("鞋型", item.Cells[0].Value.ToString());
                dic.Add("ART", item.Cells[1].Value.ToString());
                selectlist.Add(dic);
            }
            if (selectlist.Count <= 0)
            {
                MessageBox.Show("Please select data！");
                return;
            }
            this.Close();
        }

        private void dataGridViewEx1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "Column1")
            {
                List<string> art_list = new List<string>();
                if (!string.IsNullOrEmpty(_ART))
                {
                    art_list = _ART.Split(',').ToList();
                }
                var currCheck = dataGridViewEx1.Columns[e.ColumnIndex].HeaderCell.Value.ToString();
                if (currCheck.ToLower() == "true")
                {
                    foreach (DataGridViewRow item in dataGridViewEx1.Rows)
                    {
                        art_list.Add(item.Cells[3].Value.ToString());
                    }
                }
                else
                {
                    foreach (DataGridViewRow item in dataGridViewEx1.Rows)
                    {
                        art_list.Remove(item.Cells[3].Value.ToString());
                    }
                }
                if (art_list.Count > 0)
                {
                    art_list = art_list.Distinct().ToList();
                    _ART = string.Join(",", art_list);
                }
                else
                {
                    _ART = "";
                }
                updatedata();
            }
        }
        private static decimal dateDiff(DateTime dtStart, DateTime dtEnd)
        {
            //TimeSpan tsStart = new TimeSpan(dtStart.Ticks);
            //TimeSpan tsEnd = new TimeSpan(dtEnd.Ticks);
            //TimeSpan ts = tsEnd - tsStart;
            //var cc = ts.TotalSeconds / 10000000;
            // decimal hour = 24.00M;
            // decimal second = 60.00M;
            //decimal dateDiffSecond = Convert.ToDecimal(ts.Days)  * hour * second * second + Convert.ToDecimal(ts.Hours) * second * second + Convert.ToDecimal(ts.Minutes) * second + Convert.ToDecimal(ts.Seconds);
            decimal sec = (decimal)(dtEnd - dtStart).TotalMilliseconds/1000M;
            //两个时间的秒差
            return sec;
        }
    }
}
