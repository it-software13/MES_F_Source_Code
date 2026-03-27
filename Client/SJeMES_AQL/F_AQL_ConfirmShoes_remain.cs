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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_AQL
{
    public partial class F_AQL_ConfirmShoes_remain : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string _sql = string.Empty;
        public string _ART = string.Empty;
        public F_AQL_ConfirmShoes_Store _ff;
        public DataTable _dt;
        public F_AQL_ConfirmShoes_remain(F_AQL_ConfirmShoes_Store ff)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            InitDateTimePicker(starttime);
            InitDateTimePicker(endtime);
            _ff = ff;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        public F_AQL_ConfirmShoes_remain(string sql)
        {
            InitializeComponent();
            _sql = sql;
        }

        private void F_AQL_ConfirmShoes_remain_Load(object sender, EventArgs e)
        {
            AutoSizeColumn(this.dataGridViewEx1);
            AutoSizeColumn(this.dataGridViewEx2);
            GetDataList();
        }
        public void GetDataList(int indexRow = -1)
        {

            dataGridViewEx1.Rows.Clear();
            dataGridViewEx2.Rows.Clear();
            //List<string> art_list = new List<string>();
            string starttime = string.Empty;
            string endtime = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.starttime.Text))
            {
                starttime = Convert.ToDateTime(this.starttime.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrWhiteSpace(this.endtime.Text))
            {
                endtime = Convert.ToDateTime(this.endtime.Value).ToString("yyyy-MM-dd");
            }

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("MODULE_TYPE",_ff.MODULE_TYPE);
            p.Add("shoe_name", txt_shoes.Text);
            p.Add("prod_no", txt_art.Text);
            p.Add("confirm_by", txt_confirm.Text);

            p.Add("starttime", starttime);
            p.Add("endtime", endtime);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_AQLAPI",//类库名
                                        "SJ_AQLAPI.AQL_ConfirmShoes",//类名
                                        "GetConfirmShoes_Store_cz",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //视图数据显示
            _dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

            if (_dt.Rows.Count > 0)
            {
                int i = 0;
                var list = _ART.Split(',').ToArray();
                dataGridViewEx1.Rows.Clear();
                dataGridViewEx2.Rows.Clear();


                if (_dt != null && _dt.Rows.Count > 0)
                {
                    string search = txt_art.Text;
                    if (!string.IsNullOrEmpty(search))
                    {
                        var search_dt_rows = _dt.Select($@"prod_no like '%{search}%'");
                        if (search_dt_rows.Length > 0)
                        {
                            foreach (DataRow item in search_dt_rows)
                            {
                                int x = dataGridViewEx1.Rows.Add();
                                dataGridViewEx1.Rows[x].Cells[0].Value = "False";
                                dataGridViewEx1.Rows[x].Cells["id"].Value = item["aid"].ToString();
                                dataGridViewEx1.Rows[x].Cells["RN"].Value = item["RN"].ToString();
                                dataGridViewEx1.Rows[x].Cells["shoe_name"].Value = item["shoe_name"].ToString();
                                dataGridViewEx1.Rows[x].Cells["prod_no"].Value = item["prod_no"].ToString();
                                dataGridViewEx1.Rows[x].Cells["state"].Value = item["state"].ToString();

                                dataGridViewEx1.Rows[x].Cells["received_time"].Value = item["received_time"].ToString();
                                dataGridViewEx1.Rows[x].Cells["confirm_by"].Value = item["confirm_by"].ToString();
                                dataGridViewEx1.Rows[x].Cells["redo_reason"].Value = item["redo_reason"].ToString();


                                dataGridViewEx1.Rows[x].Cells["scrap_life"].Value = item["scrap_life"].ToString();
                                dataGridViewEx1.Rows[x].Cells["confirmation_time"].Value = item["confirmation_time"].ToString();
                                dataGridViewEx1.Rows[x].Cells["wh_date"].Value = item["wh_date"].ToString();
                                dataGridViewEx1.Rows[x].Cells["reminder_duration"].Value = item["reminder_duration"].ToString();
                                dataGridViewEx1.Rows[x].Cells["FOOT"].Value = item["FOOT"].ToString();

                            }
                        }
                    }
                    else
                    {
                        foreach (DataRow item in _dt.Rows)
                        {
                            int x = dataGridViewEx1.Rows.Add();
                            dataGridViewEx1.Rows[x].Cells[0].Value = "False";
                            dataGridViewEx1.Rows[x].Cells["id"].Value = item["aid"].ToString();
                            dataGridViewEx1.Rows[x].Cells["RN"].Value = item["RN"].ToString();
                            dataGridViewEx1.Rows[x].Cells["shoe_name"].Value = item["shoe_name"].ToString();
                            dataGridViewEx1.Rows[x].Cells["prod_no"].Value = item["prod_no"].ToString();
                            dataGridViewEx1.Rows[x].Cells["state"].Value = item["state"].ToString();

                            dataGridViewEx1.Rows[x].Cells["received_time"].Value = item["received_time"].ToString();
                            dataGridViewEx1.Rows[x].Cells["confirm_by"].Value = item["confirm_by"].ToString();
                            dataGridViewEx1.Rows[x].Cells["redo_reason"].Value = item["redo_reason"].ToString();

                            dataGridViewEx1.Rows[x].Cells["scrap_life"].Value = item["scrap_life"].ToString();
                            dataGridViewEx1.Rows[x].Cells["confirmation_time"].Value = item["confirmation_time"].ToString();
                            dataGridViewEx1.Rows[x].Cells["wh_date"].Value = item["wh_date"].ToString();
                            dataGridViewEx1.Rows[x].Cells["reminder_duration"].Value = item["reminder_duration"].ToString();
                            dataGridViewEx1.Rows[x].Cells["FOOT"].Value = item["FOOT"].ToString();
                        }
                    }

                }
            }
            LoadDgv(dataGridViewEx1);


        }
        public void LoadDgv(DataGridView dgv)
        {
            int widths = 0;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);  // 自动调整列宽
                widths += dgv.Columns[i].Width;   // 计算调整列后单元列的宽度和                     
            }
            if (widths >= dgv.Size.Width)  // 如果调整列的宽度大于设定列宽
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;  // 调整列的模式 自动
            else
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;  // 如果小于 则填充
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
                    if (!string.IsNullOrEmpty(_ART))
                    {
                        art_list = _ART.Split(',').ToList();
                    }
                    var currCheck = dataGridViewEx1.Rows[e.RowIndex].Cells["Column1"].Value.ToString();
                    var art = dataGridViewEx1.Rows[e.RowIndex].Cells["prod_no"].Value.ToString();
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

                }
            }
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
                        art_list.Add(item.Cells["prod_no"].Value.ToString());
                    }
                }
                else
                {
                    foreach (DataGridViewRow item in dataGridViewEx1.Rows)
                    {
                        art_list.Remove(item.Cells["prod_no"].Value.ToString());
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
        private void updatedata()
        {
            dataGridViewEx2.Rows.Clear();

            if (_dt != null && _dt.Rows.Count > 0)
            {
                var list = _ART.Split(',');
                foreach (var art in list)
                {
                    var search_dt_rows = _dt.Select($@"prod_no = '{art}'");
                    if (search_dt_rows.Length > 0)
                    {
                        foreach (DataRow item in search_dt_rows)
                        {
                            int x = dataGridViewEx2.Rows.Add();
                            //dataGridViewEx2.Rows[x].Cells[0].Value = "False";
                            //dataGridViewEx2.Rows[x].Cells[1].Value = item["RN"].ToString();
                            dataGridViewEx2.Rows[x].Cells["aid"].Value =           item["aid"].ToString();
                            dataGridViewEx2.Rows[x].Cells["shoe_name2"].Value =    item["shoe_name"].ToString();

                            dataGridViewEx2.Rows[x].Cells["STOCK_CODE2"].Value =    item["STOCK_CODE"].ToString();
                            dataGridViewEx2.Rows[x].Cells["STOCK_NAME2"].Value =    item["STOCK_NAME"].ToString();

                            dataGridViewEx2.Rows[x].Cells["prod_no2"].Value =       item["prod_no"].ToString();
                            dataGridViewEx2.Rows[x].Cells["state2"].Value =        item["state"].ToString();
                                          
                            dataGridViewEx2.Rows[x].Cells["received_time2"].Value= item["received_time"].ToString();
                            dataGridViewEx2.Rows[x].Cells["confirm_by2"].Value =     item["confirm_by"].ToString();
                            dataGridViewEx2.Rows[x].Cells["redo_reason2"].Value =   item["redo_reason"].ToString();


                            dataGridViewEx2.Rows[x].Cells["scrap_life2"].Value = item["scrap_life"].ToString();
                            dataGridViewEx2.Rows[x].Cells["confirmation_time2"].Value = item["confirmation_time"].ToString();
                            dataGridViewEx2.Rows[x].Cells["wh_date2"].Value = item["wh_date"].ToString();
                            dataGridViewEx2.Rows[x].Cells["reminder_duration2"].Value = item["reminder_duration"].ToString();
                            dataGridViewEx2.Rows[x].Cells["FOOT2"].Value = item["FOOT"].ToString();
                        }
                    }
                }

            }
            LoadDgv(dataGridViewEx2);

        }
        public void AutoSizeColumn(DataGridView DGVFiles)
        {
            int width = 0;
            //使列自适应宽度
            //对于每一列都调整
            for (int i = 0; i < DGVFiles.Columns.Count; i++)
            {
                //将每一列都调整为自动适应模式
                DGVFiles.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                //记录整个的宽度
                width += DGVFiles.Columns[i].Width;
            }
            //判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度
            //则将每列都自动调整模式设置为显示的列即可
            //如果是小于原来设定的宽度，将模式改为填充
            if (width > DGVFiles.Size.Width)
            {
                DGVFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                DGVFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            //冻结某列从左开始
            DGVFiles.Columns[1].Frozen = true;
        }

        #region 日期控件初始为空值处理

        /// <summary>
        /// 初始化日期时间控件
        /// </summary>
        /// <param name="dtp"></param>
        public static void InitDateTimePicker(DateTimePicker dtp)
        {
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = " ";  //必须设置成" "
            dtp.ValueChanged -= DateTimePicker_ValueChanged;
            dtp.ValueChanged += DateTimePicker_ValueChanged;
            dtp.KeyPress -= DateTimePicker_KeyPress;
            dtp.KeyPress += DateTimePicker_KeyPress;
        }

        public static void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "yyyy-MM-dd"; //null;
            dtp.Checked = false;// 解决BUG ：防止日期控件不能选择相同日期的 --- 要放置在设置格式之后
        }

        public static void DateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8)  // backspace左删除键
            {
                DateTimePicker dtp = (DateTimePicker)sender;
                dtp.Format = DateTimePickerFormat.Custom;
                dtp.CustomFormat = " ";
            }
        }
        #endregion

        private void btn_search_Click(object sender, EventArgs e)
        {
            AutoSizeColumn(this.dataGridViewEx1);
            AutoSizeColumn(this.dataGridViewEx2);
            GetDataList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            List<Dictionary<string, object>> diclist = new List<Dictionary<string, object>>();
            foreach (DataGridViewRow item in dataGridViewEx2.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("id", item.Cells["aid"].Value.ToString());
                dic.Add("shoe_name", item.Cells["shoe_name2"].Value.ToString());
                dic.Add("prod_no", item.Cells["prod_no2"].Value.ToString());
                dic.Add("STOCK_CODE", item.Cells["STOCK_CODE2"].Value.ToString());
                dic.Add("STOCK_NAME", item.Cells["STOCK_NAME2"].Value.ToString());


                dic.Add("scrap_life", item.Cells["scrap_life2"].Value.ToString());
                dic.Add("confirmation_time", item.Cells["confirmation_time2"].Value.ToString());
                dic.Add("wh_date", item.Cells["wh_date2"].Value.ToString());
                dic.Add("reminder_duration", item.Cells["reminder_duration2"].Value.ToString());
                dic.Add("FOOT", item.Cells["FOOT2"].Value.ToString());

                diclist.Add(dic);
            }
            if (diclist.Count <= 0)
            {
                MessageBox.Show("请选择数据！");
                return;
            }


            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("request", diclist);
            p.Add("MODULE_TYPE", _ff.MODULE_TYPE);
            p.Add("ascription", "0");//0-实验室 1原材料


            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_AQLAPI",//类库名
                                        "SJ_AQLAPI.AQL_ConfirmShoes",//类名
                                        "AddConfirmShoes_Store_rk",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("提交成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                _ff.F_AQL_ConfirmShoes_Store_Load(null,null);
                this.Close();
            }
        }
    }
}
