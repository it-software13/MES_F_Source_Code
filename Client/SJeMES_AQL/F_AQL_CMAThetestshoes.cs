using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SJeMES_AQL
{
    public partial class F_AQL_CMAThetestshoes : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private List<string> art_list = new List<string>();
        private DataTable dt_mx = new DataTable();
        public F_AQL_CMAThetestshoes()
        {
            InitializeComponent();
            InitDateTimePicker(dateTimeP_putin_date);
            InitDateTimePicker(dateTimeP_end_date);
            InitDateTimePicker(dateTimeP_putin_date1);
            InitDateTimePicker(dateTimeP_end_date2);
            InitDateTimePicker(dateTimePicker1);
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        public void F_AQL_CMAThetestshoes_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            pageControl1.BindPageEvent += GetMain_List;
            LoadPage();


            int widths = 0;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);  // 自动调整列宽
                widths += dataGridView1.Columns[i].Width;   // 计算调整列后单元列的宽度和                     
            }
            if (widths >= dataGridView1.Size.Width)  // 如果调整列的宽度大于设定列宽
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;  // 调整列的模式 自动
            else
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;  // 如果小于 则填充
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
        public void LoadPage()
        {
            pageControl1.PageSize = 10;
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        public DataTable CurrSearchDt;
        public void GetMain_List(int pageSize, int pageIndex, out int totalCount)
        {
            art_list = new List<string>();
            totalCount = 0;
            try
            {
                string putin_date = string.Empty;
                string end_date = string.Empty;
                string putin_date1 = string.Empty;
                string end_date2 = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.dateTimeP_putin_date.Text))
                {
                    putin_date = Convert.ToDateTime(this.dateTimeP_putin_date.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.dateTimeP_end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.dateTimeP_end_date.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.dateTimeP_putin_date1.Text))
                {
                    putin_date1 = Convert.ToDateTime(this.dateTimeP_putin_date1.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.dateTimeP_end_date2.Text))
                {
                    end_date2 = Convert.ToDateTime(this.dateTimeP_end_date2.Value).ToString("yyyy-MM-dd");
                }
                string status = string.Empty;
                if (ucRadioButton1.Checked)
                {
                    status = "0";//已送测
                }
                else if (ucRadioButton2.Checked)
                {
                    status = "1";//未送测
                }
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("putin_date", putin_date);
                data.Add("end_date", end_date);
                data.Add("putin_date1", putin_date1);
                data.Add("end_date2", end_date2);
                data.Add("status", status);
                data.Add("art_no", txt_art.Text);
                data.Add("name_t", txt_artname.Text);
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_CMAThetestshoes",//类名
                                            "Get_Main",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                CurrSearchDt = dt.Copy();
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["ID"].Value = dr["ID"].ToString();//鞋型
                        dgvr.Cells["name_t"].Value = dr["name_t"].ToString();//鞋型
                        dgvr.Cells["prod_no"].Value = dr["prod_no"].ToString();//art
                        dgvr.Cells["internal_test_date"].Value = dr["internal_test_date"].ToString();//实验室实际内测日期
                        dgvr.Cells["internal_test_res"].Value = dr["internal_test_res"].ToString();//内测结果勾选
                        dgvr.Cells["external_test_date"].Value = dr["external_test_date"].ToString();//外部送测日期 / 外部送测日期(有效期三个月)
                        dgvr.Cells["external_test_res"].Value = dr["external_test_res"].ToString();//外部不通过（勾选）/ 外部测试结果
                        dgvr.Cells["re_delivery_date"].Value = dr["re_delivery_date"].ToString();//再次送测日期 / 复测日期
                        dgvr.Cells["RE_TEST_RES"].Value = dr["RE_TEST_RES"].ToString();//复测结果
                        dgvr.Cells["import_date"].Value = dr["import_date"].ToString();//导入日期
                        art_list.Add(dr["prod_no"].ToString());
                        i++;
                    }
                    dt_mx = GetDgvToTable(dataGridView1);
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
        private int j = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                List<string> list = new List<string>();
                using (F_AQL_CMAThetestshoesArtEdit frm = new F_AQL_CMAThetestshoesArtEdit(this))
                {
                    frm.ShowDialog();
                    list = frm.list.Where(x => x.Count() > 0).Distinct().ToList();
                }
               

                #region old
                /*
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("list", list);//art_list
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_CMAThetestshoes", "Get_artlist", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                if (ret.IsSuccess)
                {
                    //视图数据显示
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        j = dataGridView1.RowCount;
                        string dtime = DateTime.Now.ToString("yyyy-MM-dd");
                        foreach (DataRow dr in dt.Rows)
                        {
                            art_list.Add(dr["prod_no"].ToString());
                            if (art_list.Distinct().Count() != art_list.Count)
                            {
                                art_list = art_list.Distinct().ToList();
                                continue;
                            }
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[j];
                            dgvr.Cells["NAME_T"].Value = dr["name_t"].ToString();//鞋型
                            dgvr.Cells["PROD_NO"].Value = dr["prod_no"].ToString();//art
                            dgvr.Cells["IMPORT_DATE"].Value = dtime;

                            j++;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(ret.ErrMsg);
                }
                */
                #endregion
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }



        }



        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dt_data = GetDgvToTable(dataGridView1);
                if (dt_data.Rows.Count > 0)
                {
                    string[] keyhread = { "operation" };
                    for (int i = 0; i < keyhread.Length; i++)
                    {
                        if (dt_data.Columns.Contains(keyhread[i]))
                        {
                            dt_data.Columns.Remove(keyhread[i]);
                        }
                    }
                }
                if (dt_mx.Rows.Count > 0)
                {
                    string[] keyhread2 = { "RN", "ID", "operation" };
                    for (int i = 0; i < keyhread2.Length; i++)
                    {
                        if (dt_mx.Columns.Contains(keyhread2[i]))
                        {
                            dt_mx.Columns.Remove(keyhread2[i]);
                        }
                    }
                }
                IEnumerable<DataRow> except = dt_data.AsEnumerable().Except(dt_mx.AsEnumerable(), DataRowComparer<DataRow>.Default);
                DataTable dt_except = new DataTable();
                if (except.Count() > 0)
                {
                    dt_except = except.CopyToDataTable();
                }
                if (dt_except.Rows.Count > 0)
                {
                    var diff_dt = dt_except.Clone();

                    foreach (DataRow item in dt_except.Rows)
                    {
                        string currId = item["ID"].ToString();
                        DataRow oldRow = CurrSearchDt.Select($@"ID='{currId}'")[0];

                        //对比新旧行数据是否一致
                        foreach (DataColumn dt_col in diff_dt.Columns)
                        {
                            string oldValue = string.IsNullOrEmpty(oldRow[dt_col.ColumnName].ToString()) ? "" : oldRow[dt_col.ColumnName].ToString();
                            string newValue = string.IsNullOrEmpty(item[dt_col.ColumnName].ToString()) ? "" : item[dt_col.ColumnName].ToString();
                            // 不一样，则将新行加入到diff_dt中
                            if (newValue != oldValue)
                            {
                                diff_dt.Rows.Add(item.ItemArray);
                                break;
                            }
                        }

                    }

                    if (diff_dt.Rows.Count > 0)
                    {
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("dt_data", diff_dt);
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                 "SJ_AQLAPI", "SJ_AQLAPI.AQL_CMAThetestshoes", "Commit_data", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                        if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                        {
                            string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                            LoadPage();
                        }
                        else
                            throw new Exception(j["ErrMsg"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            LoadPage();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                    if (name == "operation")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;

                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("select"))//查看明细
                        {
                            string atr_no = dataGridView1.CurrentRow.Cells["PROD_NO"].Value.ToString();
                            using (F_AQL_CMAThetestshoesMin frm = new F_AQL_CMAThetestshoesMin(atr_no))
                            {
                                frm.ShowDialog();
                                LoadPage();
                            }

                        }
                    }
                    string value = string.Empty;
                    
                    //if (dataGridView1.Columns[e.ColumnIndex].Name == "INTERNAL_TEST_DATE") // 检验结果 
                    //{
                    //    value = dataGridView1.CurrentRow.Cells["INTERNAL_TEST_DATE"].Value.ToString().Trim();
                    //    if (!string.IsNullOrEmpty(value))
                    //        dateTimePicker1.Text = value;
                    //     Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    //    dateTimePicker1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    //    dateTimePicker1.Visible = true;
                    //}
                    //else 
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "EXTERNAL_TEST_DATE") // 检验结果 
                    {
                        value = dataGridView1.CurrentRow.Cells["EXTERNAL_TEST_DATE"].Value.ToString().Trim();
                        if (!string.IsNullOrEmpty(value))
                            dateTimePicker1.Text = value;
                        Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                        dateTimePicker1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        dateTimePicker1.Visible = true;
                    }
                    else if (dataGridView1.Columns[e.ColumnIndex].Name == "RE_DELIVERY_DATE") // 检验结果 
                    {
                        value = dataGridView1.CurrentRow.Cells["RE_DELIVERY_DATE"].Value.ToString().Trim();
                        if (!string.IsNullOrEmpty(value))
                            dateTimePicker1.Text = value;
                        Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                        dateTimePicker1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        dateTimePicker1.Visible = true;
                    }
                    else
                    {
                        //dataGridView1.CurrentRow.Cells[ename].Value = value;
                        dateTimePicker1.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            //dataGridView1.CurrentCell.Value = this.dateTimePicker1.Text;
            //dateTimePicker1.Visible = false;
        }
        public static DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void dateTimePicker1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = this.dateTimePicker1.Text;
            dateTimePicker1.Visible = false;
        }
    }
}
