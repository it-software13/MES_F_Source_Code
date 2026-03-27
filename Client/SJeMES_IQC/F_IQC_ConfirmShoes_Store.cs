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
using static SJeMES_IQC.F_IQC_VWarehouse_Main;

namespace SJeMES_IQC
{
    public partial class F_IQC_ConfirmShoes_Store : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_IQC_ConfirmShoes_Store()
        {
            InitializeComponent();
            InitDateTimePicker(dateTimePicker1);
            InitDateTimePicker(dateTimePicker2);
            InitDateTimePicker(dateTimePicker3);
            InitDateTimePicker(dateTimePicker4);
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "   ";

            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker2.CustomFormat = "   ";

            this.dateTimePicker3.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker3.CustomFormat = "   ";

            this.dateTimePicker4.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker4.CustomFormat = "   ";
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

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        /// <summary>
        /// 初始化分页
        /// </summary>
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// 查询-确认鞋-存放管理-主页-状态
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetConfirmShoes_Store_Main_zt()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_ConfirmShoes",//类名
                                            "GetConfirmShoes_Store_Main_zt",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                checkedListBox1.DataSource = dt;
                checkedListBox1.DisplayMember = "value";
                checkedListBox1.ValueMember = "code";
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 查询-确认鞋-存放管理-主页
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetConfirmShoes_Store_Main(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("shoe_name", textBox1.Text.Trim());
                p.Add("prod_name", textBox2.Text.Trim());
                p.Add("stock_name", textBox4.Text.Trim());
                if (!string.IsNullOrWhiteSpace(dateTimePicker1.Text.ToString()))
                {
                    p.Add("wh_dateS", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                }
                if (!string.IsNullOrWhiteSpace(dateTimePicker2.Text.ToString()))
                {
                    p.Add("wh_dateE", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                }
                if (!string.IsNullOrWhiteSpace(dateTimePicker3.Text.ToString()))
                {
                    p.Add("output_dateS", dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                }
                if (!string.IsNullOrWhiteSpace(dateTimePicker4.Text.ToString()))
                {
                    p.Add("output_dateE", dateTimePicker4.Value.ToString("yyyy-MM-dd"));
                }
                List<string> ref_standard = new List<string>();
                foreach (System.Data.DataRowView item in this.checkedListBox1.CheckedItems)
                {
                    ref_standard.Add(item.Row["code"].ToString());
                }
                p.Add("ref_standard", ref_standard);
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_ConfirmShoes",//类名
                                            "GetConfirmShoes_Store_Main",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridViewEx1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridViewEx1.Rows.Add();
                        DataGridViewRow dgvr = dataGridViewEx1.Rows[i];
                        dgvr.Cells["qid"].Value = dr["qid"].ToString();
                        dgvr.Cells["xh"].Value = i + 1;
                        dgvr.Cells["shoe_no"].Value = dr["shoe_no"].ToString();
                        dgvr.Cells["shoe_name"].Value = dr["shoe_name"].ToString();
                        dgvr.Cells["prod_no"].Value = dr["prod_no"].ToString();
                        dgvr.Cells["prod_name"].Value = dr["prod_name"].ToString();
                        dgvr.Cells["stock_code"].Value = dr["stock_code"].ToString();
                        dgvr.Cells["stock_name"].Value = dr["stock_name"].ToString();
                        dgvr.Cells["ref_standard"].Value = dr["ref_standard_name"].ToString();
                        dgvr.Cells["wh_date"].Value = dr["wh_date"].ToString();
                        dgvr.Cells["output_date"].Value = dr["output_date"].ToString();
                        dgvr.Cells["reconfirmation_time"].Value = dr["reconfirmation_time"].ToString();
                        dgvr.Cells["expected_maturity_date"].Value = dr["expected_maturity_date"].ToString();
                        switch (dr["ref_standard"].ToString())
                        {
                            case "0":
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["gh"]).Enabled = false;
                                break;
                            case "1":
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["gh"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["bf"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["jc"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["ck"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["zqr"]).Enabled = false;
                                break;
                            case "2":
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["bf"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["jc"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["ck"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["zqr"]).Enabled = false;
                                break;
                            case "4":
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["bf"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["gh"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["ck"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["zqr"]).Enabled = false;
                                break;
                            case "5":
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["gh"]).Enabled = false;
                                break;
                            default:
                                break;
                        }
                        if (dr["czbz"].ToString() == "0")
                        {
                            string wh_date = dr["wh_date"].ToString()==""?DateTime.Now.ToString("yyyy-MM-dd"): dr["wh_date"].ToString();
                            TimeSpan ts = Convert.ToDateTime(dr["expected_maturity_date"].ToString()) - Convert.ToDateTime(wh_date);
                            if ((ts.Days - Convert.ToInt32(dr["remind_day"].ToString())) <= 0)
                            {
                                dataGridViewEx1.Rows[i].Cells["expected_maturity_date"].Style.ForeColor = Color.Red;
                            }
                        }
                        if (dr["czbz"].ToString() == "1")
                        {
                            string output_date= dr["output_date"].ToString() == "" ? DateTime.Now.ToString("yyyy-MM-dd") : dr["output_date"].ToString();
                            TimeSpan ts = Convert.ToDateTime(dr["expected_maturity_date"].ToString()) - Convert.ToDateTime(output_date);
                            if ((ts.Days - Convert.ToInt32(dr["remind_day"].ToString())) <= 0)
                            {
                                dataGridViewEx1.Rows[i].Cells["expected_maturity_date"].Style.ForeColor = Color.Red;
                            }
                        }
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridViewEx1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void F_IQC_ConfirmShoes_Store_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            GetConfirmShoes_Store_Main_zt();

            pageControl1.BindPageEvent += GetConfirmShoes_Store_Main;
            LoadPage();
            this.dataGridViewEx1.ClearSelection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (F_IQC_ConfirmShoes_Store_Add c = new F_IQC_ConfirmShoes_Store_Add())
            {
                c.ShowDialog();
            }
            LoadPage();
        }

        private void dataGridViewEx1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "bf" && ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[e.RowIndex].Cells["bf"]).Enabled)
                {
                    string qid = dataGridViewEx1.Rows[e.RowIndex].Cells["qid"].Value.ToString();
                    EditConfirmShoes_Store_bf_ck("1",qid);
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "ck" && ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[e.RowIndex].Cells["ck"]).Enabled)
                {
                    string qid = dataGridViewEx1.Rows[e.RowIndex].Cells["qid"].Value.ToString();
                    EditConfirmShoes_Store_bf_ck("3", qid);
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "zqr" && ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[e.RowIndex].Cells["zqr"]).Enabled)
                {
                    string qid = dataGridViewEx1.Rows[e.RowIndex].Cells["qid"].Value.ToString();
                    EditConfirmShoes_Store_zqr("5", qid);
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "sc" && ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[e.RowIndex].Cells["sc"]).Enabled)
                {
                    string qid = dataGridViewEx1.Rows[e.RowIndex].Cells["qid"].Value.ToString();
                    DeleteConfirmShoes_Store(qid);
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "jc" && ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[e.RowIndex].Cells["jc"]).Enabled)
                {
                    string qid = dataGridViewEx1.Rows[e.RowIndex].Cells["qid"].Value.ToString();
                    using (F_IQC_ConfirmShoes_Store_jcgh c=new F_IQC_ConfirmShoes_Store_jcgh(qid,"2"))
                    {
                        c.Text = "借出人员";
                        c.ShowDialog();
                    }
                    LoadPage();
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "gh" && ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[e.RowIndex].Cells["gh"]).Enabled)
                {
                    string qid = dataGridViewEx1.Rows[e.RowIndex].Cells["qid"].Value.ToString();
                    using (F_IQC_ConfirmShoes_Store_jcgh c = new F_IQC_ConfirmShoes_Store_jcgh(qid, "0"))
                    {
                        c.Text = "归还人员";
                        c.ShowDialog();
                    }
                    LoadPage();
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "czjl")
                {
                    string qid = dataGridViewEx1.Rows[e.RowIndex].Cells["qid"].Value.ToString();
                    using (F_IQC_ConfirmShoes_Store_State c = new F_IQC_ConfirmShoes_Store_State(qid))
                    {
                        c.ShowDialog();
                    }
                }
            }
        }

        /// <summary>
        /// 编辑-确认鞋-存放管理_报废/出库
        /// </summary>
        public void EditConfirmShoes_Store_bf_ck(string ref_standard,string qid)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("ref_standard", ref_standard);
                data.Add("qid", qid);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJeMES_IQC", "SJeMES_IQC.IQC_ConfirmShoes", "EditConfirmShoes_Store_bf_ck", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("编辑成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    LoadPage();
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 编辑-确认鞋-存放管理_再确认
        /// </summary>
        public void EditConfirmShoes_Store_zqr(string ref_standard, string qid)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("ref_standard", ref_standard);
                data.Add("qid", qid);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJeMES_IQC", "SJeMES_IQC.IQC_ConfirmShoes", "EditConfirmShoes_Store_zqr", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("编辑成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    LoadPage();
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 删除-确认鞋-存放管理
        /// </summary>
        public void DeleteConfirmShoes_Store(string qid)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("qid", qid);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJeMES_IQC", "SJeMES_IQC.IQC_ConfirmShoes", "DeleteConfirmShoes_Store", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("删除成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    LoadPage();
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// dgv控件转datatable
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public DataTable GetDgvToTable(DataGridView dgv)
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

        /// <summary>
        /// 编辑-确认鞋-存放管理-批量报废
        /// </summary>
        public void DeleteConfirmShoes_Store_plbf()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("confirm", GetDgvToTable(dataGridViewEx1));
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJeMES_IQC", "SJeMES_IQC.IQC_ConfirmShoes", "DeleteConfirmShoes_Store_plbf", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("编辑成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    LoadPage();
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteConfirmShoes_Store_plbf();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //视图数据显示
                DataTable dts = GetConfirmShoes_Store_Main_Excel();
                if (dts.Rows.Count < 1)
                {
                    MessageBox.Show("暂无数据导出，请检查是否操作正确");
                    return;
                }
                /* if (DT_EXCEL.Rows.Count < 1)
                 {
                     MessageBox.Show("数据为空，先搜索再做导出操作");
                     return;
                 }*/
                //for (int i = 0; i < dts.Rows.Count; i++)
                //{
                //    dts.Rows.RemoveAt(i);
                //}
                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                Execldic.Add("SHOE_NAME", "鞋型");
                Execldic.Add("PROD_NO", "ART代号");
                Execldic.Add("STOCK_NAME", "存放位置");
                Execldic.Add("REF_STANDARD_NAME", "状态");
                Execldic.Add("WH_DATE", "入库日期");
                Execldic.Add("OUTPUT_DATE", "量产日期");
                Execldic.Add("RECONFIRMATION_TIME", "再确认时间");
                Execldic.Add("EXPECTED_MATURITY_DATE", "预计到期日期");

                ExeclHelper.ExportToTrueExcel(dts, Execldic, "确认鞋");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 查询-确认鞋-存放管理-主页-导出
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public DataTable GetConfirmShoes_Store_Main_Excel()
        {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("shoe_name", textBox1.Text.Trim());
                p.Add("prod_name", textBox2.Text.Trim());
                p.Add("stock_name", textBox4.Text.Trim());
                if (!string.IsNullOrWhiteSpace(dateTimePicker1.Text.ToString()))
                {
                    p.Add("wh_dateS", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                }
                if (!string.IsNullOrWhiteSpace(dateTimePicker2.Text.ToString()))
                {
                    p.Add("wh_dateE", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                }
                if (!string.IsNullOrWhiteSpace(dateTimePicker3.Text.ToString()))
                {
                    p.Add("output_dateS", dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                }
                if (!string.IsNullOrWhiteSpace(dateTimePicker4.Text.ToString()))
                {
                    p.Add("output_dateE", dateTimePicker4.Value.ToString("yyyy-MM-dd"));
                }
                List<string> ref_standard = new List<string>();
                foreach (System.Data.DataRowView item in this.checkedListBox1.CheckedItems)
                {
                    ref_standard.Add(item.Row["code"].ToString());
                }
                p.Add("ref_standard", ref_standard);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_ConfirmShoes",//类名
                                            "GetConfirmShoes_Store_Main_Excel",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            return dt;
        }
    }
}
