using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.WebAPI;
using SJeMES_Report.IQC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_BDM
{
    public partial class F_BDM_Painted_Skin_Report : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string task_no = string.Empty;
        public F_BDM_Painted_Skin_Report()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public F_BDM_Painted_Skin_Report(string _task_no)
        {
            task_no = _task_no;
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        /// <summary>
        /// 初始化20行面积
        /// </summary>
        public void area()
        {
            for (int i = 0; i < 20; i++)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells["xh"].Value = i + 1;
                this.dataGridView1.Rows[index].Cells["gys_area"].Value = "";
                this.dataGridView1.Rows[index].Cells["sj_area"].Value = "";
            }
            int index2 = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index2].Cells["xh"].Value = "合计";
            this.dataGridView1.Rows[index2].Cells["gys_area"].Value = "";
            this.dataGridView1.Rows[index2].Cells["sj_area"].Value = "";
        }

        private void F_BDM_Painted_Skin_Report_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            GetPainted_Skin_Report_satra_l();
            GetPainted_Skin_Report_Head();
            GetPainted_Skin_Report_task_d();
        }

        /// <summary>
        /// 皮料评估报表页面头查询
        /// </summary>
        public void GetPainted_Skin_Report_Head()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Painted_Skin",//类名
                                            "GetPainted_Skin_Report_Head",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

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
                var dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());
                if (dt.Rows.Count > 0)
                {
                    label10.Text = dt.Rows[0]["item_no"].ToString();
                    label11.Text = dt.Rows[0]["item_name"].ToString();
                    label12.Text = dt.Rows[0]["qty"].ToString();
                    label14.Text = dt.Rows[0]["CREATEDATE"].ToString();
                    label16.Text = dt.Rows[0]["ITEM_TYPE_NAME"].ToString();
                    label22.Text = dt.Rows[0]["mtl_qty"].ToString();
                    label23.Text = dt.Rows[0]["vend_name"].ToString();
                }

                if (dt1.Rows.Count>0)
                {
                    //textBox11.Text = dt1.Rows[0]["pl_qty"].ToString();
                    //textBox10.Text = dt1.Rows[0]["supplier"].ToString();
                    textBox1.Text = dt1.Rows[0]["approver"].ToString();
                    textBox2.Text = dt1.Rows[0]["tabulator"].ToString();
                    textBox3.Text = dt1.Rows[0]["area_diff_cft"].ToString();
                    textBox4.Text = dt1.Rows[0]["pur_qty_cft"].ToString();
                    textBox5.Text = dt1.Rows[0]["avg_use_rate"].ToString();
                    richTextBox1.Text = dt1.Rows[0]["assessment"].ToString();
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 皮料评估报表页面画皮记录查询
        /// </summary>
        public void GetPainted_Skin_Report_task_d()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task_no);
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Painted_Skin",//类名
                                            "GetPainted_Skin_Report_task_d",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

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
                dataGridView2.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView2.Rows.Add();
                        DataGridViewRow dgvr = dataGridView2.Rows[i];
                        dgvr.Cells["id"].Value = dr["id"].ToString();
                        dgvr.Cells["enum_code"].Value = dr["enum_code"].ToString();
                        dgvr.Cells["pl_level"].Value = dr["enum_value"].ToString();
                        dgvr.Cells["qty"].Value = dr["qty"].ToString();
                        dgvr.Cells["coefficient"].Value = dr["coefficient"].ToString() == "-" ? "-":(Convert.ToDecimal(dr["coefficient"].ToString()) *100)+"%";
                        dgvr.Cells["multiple"].Value = dr["multiple"].ToString();
                        dgvr.Cells["isinput"].Value = dr["isinput"].ToString();
                        dgvr.Cells["istotal"].Value = dr["istotal"].ToString();
                        i++;
                    }
                    int index2 = this.dataGridView2.Rows.Add();
                    this.dataGridView2.Rows[index2].Cells["id"].Value = "0";
                    this.dataGridView2.Rows[index2].Cells["enum_code"].Value = "总数";
                    this.dataGridView2.Rows[index2].Cells["pl_level"].Value = "总数";
                    this.dataGridView2.Rows[index2].Cells["qty"].Value = "";
                    this.dataGridView2.Rows[index2].Cells["coefficient"].Value = "-";
                    this.dataGridView2.Rows[index2].Cells["multiple"].Value = "-";
                    this.dataGridView2.Rows[index2].Cells["isinput"].Value = "false";
                    this.dataGridView2.Rows[index2].Cells["istotal"].Value = "true";
                }
                dataGridView2.ClearSelection();

                decimal count = 0;
                for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                {
                    if (!string.IsNullOrWhiteSpace(dataGridView2.Rows[i].Cells["qty"].Value.ToString()) && dataGridView2.Rows[i].Cells["pl_level"].Value.ToString() != "I~V总和")
                    {
                        count += Convert.ToDecimal(dataGridView2.Rows[i].Cells["qty"].Value.ToString());
                    }
                }
                dataGridView2.Rows[dataGridView2.RowCount - 1].Cells["qty"].Value = count;


                //decimal count1 = 0;
                //for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                //{
                //    if (!string.IsNullOrWhiteSpace(dataGridView2.Rows[i].Cells["multiple"].Value.ToString()) && dataGridView2.Rows[i].Cells["pl_level"].Value.ToString() != "I~V总和")
                //    {
                //        count1 += Convert.ToDecimal(dataGridView2.Rows[i].Cells["multiple"].Value.ToString());
                //    }
                //}
                //dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["multiple"].Value = count1;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 皮料评估报表页面面积抽检查询
        /// </summary>
        public void GetPainted_Skin_Report_satra_l()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task_no);
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Painted_Skin",//类名
                                            "GetPainted_Skin_Report_satra_l",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

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
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["xh"].Value = dr["sorting"].ToString();
                        dgvr.Cells["gys_area"].Value = dr["supplier_area"].ToString();
                        dgvr.Cells["sj_area"].Value = dr["actual_area"].ToString();
                        i++;
                    }
                    int index2 = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index2].Cells["xh"].Value = "合计";
                    this.dataGridView1.Rows[index2].Cells["gys_area"].Value = "";
                    this.dataGridView1.Rows[index2].Cells["sj_area"].Value = "";
                    dataGridView1.ClearSelection();

                    decimal count = 0;
                    for (int a = 0; a < dataGridView1.Rows.Count-1; a++)
                    {
                        if (!string.IsNullOrEmpty(dataGridView1.Rows[a].Cells["gys_area"].Value.ToString()))
                        {
                            count += Convert.ToDecimal(dataGridView1.Rows[a].Cells["gys_area"].Value.ToString());
                        }
                    }
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["gys_area"].Value = count;

                    decimal count1 = 0;
                    for (int a = 0; a < dataGridView1.Rows.Count - 1; a++)
                    {
                        if (!string.IsNullOrEmpty(dataGridView1.Rows[a].Cells["sj_area"].Value.ToString()))
                        {
                            count1 += Convert.ToDecimal(dataGridView1.Rows[a].Cells["sj_area"].Value.ToString());
                        }
                    }
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["sj_area"].Value = count1;
                }
                else
                {
                    area();
                }
            }
            catch (Exception ex)
            { 
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "gys_area" && dataGridView1.Rows[e.RowIndex].Cells["xh"].Value.ToString() != "合计") // 供应商面积 
                {
                    textBox7.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["gys_area"].Value is null ? "" : dataGridView1.CurrentRow.Cells["gys_area"].Value.ToString();
                    string gys_area = aa == "" ? "" : aa;
                    textBox6.Text = gys_area; //供应商面积

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox6.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox6.Visible = true;
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "sj_area" && dataGridView1.Rows[e.RowIndex].Cells["xh"].Value.ToString() != "合计") // 实际面积 
                {
                    textBox6.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["sj_area"].Value is null ? "" : dataGridView1.CurrentRow.Cells["sj_area"].Value.ToString();
                    string sj_area = aa == "" ? "" : aa;
                    textBox7.Text = sj_area; //实际面积

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox7.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox7.Visible = true;
                }
                else
                {
                    textBox6.Visible = false;
                    textBox7.Visible = false;
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {

                dataGridView1.CurrentCell.Value = textBox6.Text.ToString();
                decimal count = 0;
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["gys_area"].Value.ToString()))
                    {
                        decimal addVal = 0;
                        bool convetRes = Decimal.TryParse(dataGridView1.Rows[i].Cells["gys_area"].Value.ToString(), out addVal);
                        if (convetRes)
                            count += addVal;
                        else
                            dataGridView1.Rows[i].Cells["gys_area"].Value = "";
                    }
                }
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["gys_area"].Value = count;

                decimal count2 = 0;
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["sj_area"].Value.ToString()))
                    {
                        decimal addVal = 0;
                        bool convetRes = Decimal.TryParse(dataGridView1.Rows[i].Cells["sj_area"].Value.ToString(), out addVal);
                        if (convetRes)
                            count2 += addVal;
                        else
                            dataGridView1.Rows[i].Cells["sj_area"].Value = "";
                    }
                }
                if (count > 0)
                {
                    textBox3.Text = Math.Round(((count2 - count) / count) * 100, 2).ToString();
                }
                else
                    textBox3.Text = "0";

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal count = 0;
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["gys_area"].Value.ToString()))
                    {
                        decimal addVal = 0;
                        bool convetRes = Decimal.TryParse(dataGridView1.Rows[i].Cells["gys_area"].Value.ToString(), out addVal);
                        if (convetRes)
                            count += addVal;
                        else
                            dataGridView1.Rows[i].Cells["gys_area"].Value = "";
                    }
                }

                dataGridView1.CurrentCell.Value = textBox7.Text.ToString();
                decimal count2 = 0;
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["sj_area"].Value.ToString()))
                    {
                        decimal addVal = 0;
                        bool convetRes = Decimal.TryParse(dataGridView1.Rows[i].Cells["sj_area"].Value.ToString(), out addVal);
                        if (convetRes)
                            count2 += addVal;
                        else
                            dataGridView1.Rows[i].Cells["sj_area"].Value = "";
                    }
                }
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["sj_area"].Value = count2;

                if (count > 0)
                {
                    textBox3.Text = Math.Round(((count2 - count) / count) * 100, 2).ToString();
                }
                else
                    textBox3.Text = "0";

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.') && e.KeyChar != (char)('-'))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)('-'))
            {
                if ((sender as TextBox).Text != "")
                {
                    e.Handled = true;
                }
            }
            //第1位是负号时候、第2位小数点不可
            if (((TextBox)sender).Text == "-" && e.KeyChar == (char)('.'))
            {
                e.Handled = true;
            }
            //负号只能1次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;
            //第1位小数点不可
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text == "")
            {
                e.Handled = true;
            }
            //小数点只能1次
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }
            //小数点（最大到2位）   
            if (e.KeyChar != '\b' && (((TextBox)sender).SelectionStart) > (((TextBox)sender).Text.LastIndexOf('.')) + 2 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
            //光标在小数点右侧时候判断  
            if (e.KeyChar != '\b' && ((TextBox)sender).SelectionStart >= (((TextBox)sender).Text.LastIndexOf('.')) && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 1)
                {
                    if ((((TextBox)sender).Text.Length).ToString() == (((TextBox)sender).Text.IndexOf(".") + 3).ToString())
                        e.Handled = true;
                }
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 2)
                {
                    if ((((TextBox)sender).Text.Length - 3).ToString() == ((TextBox)sender).Text.IndexOf(".").ToString()) e.Handled = true;
                }
            }
            //第1位是0，第2位必须是小数点
            if (e.KeyChar != (char)('.') && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.') && e.KeyChar != (char)('-'))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)('-'))
            {
                if ((sender as TextBox).Text != "")
                {
                    e.Handled = true;
                }
            }
            //第1位是负号时候、第2位小数点不可
            if (((TextBox)sender).Text == "-" && e.KeyChar == (char)('.'))
            {
                e.Handled = true;
            }
            //负号只能1次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;
            //第1位小数点不可
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text == "")
            {
                e.Handled = true;
            }
            //小数点只能1次
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }
            //小数点（最大到2位）   
            if (e.KeyChar != '\b' && (((TextBox)sender).SelectionStart) > (((TextBox)sender).Text.LastIndexOf('.')) + 2 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
            //光标在小数点右侧时候判断  
            if (e.KeyChar != '\b' && ((TextBox)sender).SelectionStart >= (((TextBox)sender).Text.LastIndexOf('.')) && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 1)
                {
                    if ((((TextBox)sender).Text.Length).ToString() == (((TextBox)sender).Text.IndexOf(".") + 3).ToString())
                        e.Handled = true;
                }
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 2)
                {
                    if ((((TextBox)sender).Text.Length - 3).ToString() == ((TextBox)sender).Text.IndexOf(".").ToString()) e.Handled = true;
                }
            }
            //第1位是0，第2位必须是小数点
            if (e.KeyChar != (char)('.') && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
            {
                e.Handled = true;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView2.Columns[e.ColumnIndex].Name == "qty" && dataGridView2.Rows[e.RowIndex].Cells["enum_code"].Value.ToString() != "总数" && dataGridView2.Rows[e.RowIndex].Cells["isinput"].Value.ToString()=="true") // 数量(尺) 
                {
                    textBox6.Visible = false;
                    textBox7.Visible = false;
                    textBox9.Visible = false;
                    string aa = dataGridView2.CurrentRow.Cells["qty"].Value is null ? "" : dataGridView2.CurrentRow.Cells["qty"].Value.ToString();
                    string qty = aa == "" ? "" : aa;
                    textBox8.Text = qty; //数量(尺) 

                    Rectangle R = dataGridView2.GetCellDisplayRectangle(dataGridView2.CurrentCell.ColumnIndex, dataGridView2.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox8.SetBounds(R.X + dataGridView2.Location.X, R.Y + dataGridView2.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox8.Visible = true;
                }
                else if (dataGridView2.Columns[e.ColumnIndex].Name == "multiple" && dataGridView2.Rows[e.RowIndex].Cells["enum_code"].Value.ToString() != "总数" && dataGridView2.Rows[e.RowIndex].Cells["isinput"].Value.ToString() == "true") // 倍数 
                {
                    textBox6.Visible = false;
                    textBox7.Visible = false;
                    textBox8.Visible = false;
                    string aa = dataGridView2.CurrentRow.Cells["multiple"].Value is null ? "" : dataGridView2.CurrentRow.Cells["multiple"].Value.ToString();
                    string multiple = aa == "" ? "" : aa;
                    textBox9.Text = multiple; //倍数

                    Rectangle R = dataGridView2.GetCellDisplayRectangle(dataGridView2.CurrentCell.ColumnIndex, dataGridView2.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox9.SetBounds(R.X + dataGridView2.Location.X, R.Y + dataGridView2.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox9.Visible = true;
                }
                else
                {
                    textBox6.Visible = false;
                    textBox7.Visible = false;
                    textBox8.Visible = false;
                    textBox9.Visible = false;
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.CurrentCell.Value = textBox8.Text.ToString();
            decimal count = 0;
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                if (!string.IsNullOrWhiteSpace(dataGridView2.Rows[i].Cells["qty"].Value.ToString()) && dataGridView2.Rows[i].Cells["pl_level"].Value.ToString() != "I~V总和")
                {
                    count += Convert.ToDecimal(dataGridView2.Rows[i].Cells["qty"].Value.ToString());
                }
            }
            dataGridView2.Rows[dataGridView2.RowCount - 1].Cells["qty"].Value = count;
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.') && e.KeyChar != (char)('-'))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)('-'))
            {
                if ((sender as TextBox).Text != "")
                {
                    e.Handled = true;
                }
            }
            //第1位是负号时候、第2位小数点不可
            if (((TextBox)sender).Text == "-" && e.KeyChar == (char)('.'))
            {
                e.Handled = true;
            }
            //负号只能1次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;
            //第1位小数点不可
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text == "")
            {
                e.Handled = true;
            }
            //小数点只能1次
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }
            //小数点（最大到2位）   
            if (e.KeyChar != '\b' && (((TextBox)sender).SelectionStart) > (((TextBox)sender).Text.LastIndexOf('.')) + 2 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
            //光标在小数点右侧时候判断  
            if (e.KeyChar != '\b' && ((TextBox)sender).SelectionStart >= (((TextBox)sender).Text.LastIndexOf('.')) && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 1)
                {
                    if ((((TextBox)sender).Text.Length).ToString() == (((TextBox)sender).Text.IndexOf(".") + 3).ToString())
                        e.Handled = true;
                }
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 2)
                {
                    if ((((TextBox)sender).Text.Length - 3).ToString() == ((TextBox)sender).Text.IndexOf(".").ToString()) e.Handled = true;
                }
            }
            //第1位是0，第2位必须是小数点
            //if (e.KeyChar != (char)('.') && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
            //{
            //    e.Handled = true;
            //}
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.CurrentCell.Value = textBox9.Text.ToString();
            //decimal count = 0;
            //for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            //{
            //    if (!string.IsNullOrWhiteSpace(dataGridView2.Rows[i].Cells["multiple"].Value.ToString()) && dataGridView2.Rows[i].Cells["pl_level"].Value.ToString() != "I~V总和")
            //    {
            //        count += Convert.ToDecimal(dataGridView2.Rows[i].Cells["multiple"].Value.ToString());
            //    }
            //}
            //dataGridView2.Rows[dataGridView2.RowCount - 1].Cells["multiple"].Value = count;
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.') && e.KeyChar != (char)('-'))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)('-'))
            {
                if ((sender as TextBox).Text != "")
                {
                    e.Handled = true;
                }
            }
            //第1位是负号时候、第2位小数点不可
            if (((TextBox)sender).Text == "-" && e.KeyChar == (char)('.'))
            {
                e.Handled = true;
            }
            //负号只能1次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;
            //第1位小数点不可
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text == "")
            {
                e.Handled = true;
            }
            //小数点只能1次
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }
            //小数点（最大到2位）   
            if (e.KeyChar != '\b' && (((TextBox)sender).SelectionStart) > (((TextBox)sender).Text.LastIndexOf('.')) + 2 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
            //光标在小数点右侧时候判断  
            if (e.KeyChar != '\b' && ((TextBox)sender).SelectionStart >= (((TextBox)sender).Text.LastIndexOf('.')) && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 1)
                {
                    if ((((TextBox)sender).Text.Length).ToString() == (((TextBox)sender).Text.IndexOf(".") + 3).ToString())
                        e.Handled = true;
                }
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 2)
                {
                    if ((((TextBox)sender).Text.Length - 3).ToString() == ((TextBox)sender).Text.IndexOf(".").ToString()) e.Handled = true;
                }
            }
            //第1位是0，第2位必须是小数点
            //if (e.KeyChar != (char)('.') && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
            //{
            //    e.Handled = true;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string multiple = string.Empty;//倍数
            decimal count = 0;
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                if (!string.IsNullOrWhiteSpace(dataGridView2.Rows[i].Cells["multiple"].Value.ToString()) && dataGridView2.Rows[i].Cells["pl_level"].Value.ToString() != "I~V总和"
                    && dataGridView2.Rows[i].Cells["isinput"].Value.ToString() != "true" && dataGridView2.Rows[i].Cells["istotal"].Value.ToString() != "true")
                {
                    count += Convert.ToDecimal(dataGridView2.Rows[i].Cells["multiple"].Value.ToString());
                }
            }
            multiple = count.ToString();
            decimal qtycount = 0;
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                if (!string.IsNullOrWhiteSpace(dataGridView2.Rows[i].Cells["qty"].Value.ToString()) && dataGridView2.Rows[i].Cells["pl_level"].Value.ToString() != "I~V总和"
                    && dataGridView2.Rows[i].Cells["isinput"].Value.ToString() != "true" && dataGridView2.Rows[i].Cells["istotal"].Value.ToString() != "true")
                {
                    qtycount += Convert.ToDecimal(dataGridView2.Rows[i].Cells["qty"].Value.ToString());
                }
            }
            string qty = qtycount.ToString();//数量
            string coefficient = string.Empty;
            if (multiple=="0"||string.IsNullOrEmpty(multiple))
            {
                coefficient = "0%";
            }
            else if (qty == "0" || string.IsNullOrEmpty(qty))
            {
                coefficient = "0%";
            }
            else
            {
                coefficient = Math.Round((Convert.ToDecimal(multiple) / Convert.ToDecimal(qty) * 100), 2).ToString() + "%";
            }
            if (coefficient!= textBox4.Text)
            {
                MessageBox.Show("Purchase quality coefficient data is abnormal!");
                return;
            }

            //if (string.IsNullOrWhiteSpace(textBox11.Text)|| string.IsNullOrWhiteSpace(textBox10.Text)
            //    || string.IsNullOrWhiteSpace(textBox4.Text)|| string.IsNullOrWhiteSpace(textBox5.Text)|| string.IsNullOrWhiteSpace(richTextBox1.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            //{
            //    MessageBox.Show("基础信息不能为空!");
            //    return;
            //}

            //DataTable dt2 = GetDgvToTable(dataGridView2);
            //foreach (DataRow item in dt2.Rows)
            //{
            //    if (string.IsNullOrWhiteSpace(item["multiple"].ToString())|| string.IsNullOrWhiteSpace(item["qty"].ToString()))
            //    {
            //        MessageBox.Show("数量、倍数不能为空!");
            //        return;
            //    }
            //}

            Painted_Skin_Report_Edit();
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
        /// 皮料评估报表页面保存
        /// </summary>
        public void Painted_Skin_Report_Edit()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task_no);
                Dictionary<string, string> tabHead = new Dictionary<string, string>();
                tabHead.Add("pl_qty", label22.Text);
                tabHead.Add("supplier", label23.Text);
                tabHead.Add("approver", textBox1.Text);
                tabHead.Add("tabulator", textBox2.Text);
                tabHead.Add("pur_qty_cft", textBox4.Text);
                tabHead.Add("avg_use_rate", textBox5.Text);
                tabHead.Add("assessment", richTextBox1.Text);
                tabHead.Add("area_diff_cft", textBox3.Text);
                data.Add("tabHead", tabHead);
                DataTable dt1 = GetDgvToTable(dataGridView1);
                data.Add("qcm_hp_task_satra_l", dt1);

                DataTable dt2 = GetDgvToTable(dataGridView2);
                data.Add("qcm_hp_task_satra_r", dt2);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.BDM_Painted_Skin", "Painted_Skin_Report_Edit", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {

                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);

                    //string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    //SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                    this.Close();
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

        private void button4_Click(object sender, EventArgs e)
        {
            string multiple = string.Empty; ;//倍数
            decimal count = 0;
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                if (!string.IsNullOrWhiteSpace(dataGridView2.Rows[i].Cells["multiple"].Value.ToString()) && dataGridView2.Rows[i].Cells["pl_level"].Value.ToString() != "I~V总和" 
                    && dataGridView2.Rows[i].Cells["isinput"].Value.ToString() != "true" && dataGridView2.Rows[i].Cells["istotal"].Value.ToString() != "true")
                {
                    count += Convert.ToDecimal(dataGridView2.Rows[i].Cells["multiple"].Value.ToString());
                }
            }
            multiple = count.ToString();
            decimal qtycount = 0;
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                if (!string.IsNullOrWhiteSpace(dataGridView2.Rows[i].Cells["qty"].Value.ToString()) && dataGridView2.Rows[i].Cells["pl_level"].Value.ToString() != "I~V总和"
                    && dataGridView2.Rows[i].Cells["isinput"].Value.ToString() != "true" && dataGridView2.Rows[i].Cells["istotal"].Value.ToString() != "true")
                {
                    qtycount += Convert.ToDecimal(dataGridView2.Rows[i].Cells["qty"].Value.ToString());
                }
            }
            string qty = qtycount.ToString();//数量
            if (multiple == "0" || string.IsNullOrEmpty(multiple))
            {
                textBox4.Text = "0%";
            }
            else if (qty == "0" || string.IsNullOrEmpty(qty))
            {
                textBox4.Text = "0%";
            }
            else
            {
                textBox4.Text = Math.Round((Convert.ToDecimal(multiple) / Convert.ToDecimal(qty) * 100), 2).ToString() + "%";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string multiple = string.Empty;//倍数
            decimal count = 0;
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                if (!string.IsNullOrWhiteSpace(dataGridView2.Rows[i].Cells["multiple"].Value.ToString()) && dataGridView2.Rows[i].Cells["pl_level"].Value.ToString() != "I~V总和"
                    && dataGridView2.Rows[i].Cells["isinput"].Value.ToString() != "true" && dataGridView2.Rows[i].Cells["istotal"].Value.ToString() != "true")
                {
                    count += Convert.ToDecimal(dataGridView2.Rows[i].Cells["multiple"].Value.ToString());
                }
            }
            multiple = count.ToString();
            decimal qtycount = 0;
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                if (!string.IsNullOrWhiteSpace(dataGridView2.Rows[i].Cells["qty"].Value.ToString()) && dataGridView2.Rows[i].Cells["pl_level"].Value.ToString() != "I~V总和"
                    && dataGridView2.Rows[i].Cells["isinput"].Value.ToString() != "true" && dataGridView2.Rows[i].Cells["istotal"].Value.ToString() != "true")
                {
                    qtycount += Convert.ToDecimal(dataGridView2.Rows[i].Cells["qty"].Value.ToString());
                }
            }
            string qty = qtycount.ToString();//数量
            string coefficient = string.Empty;
            if (multiple == "0" || string.IsNullOrEmpty(multiple))
            {
                coefficient = "0%";
            }
            else if (qty == "0" || string.IsNullOrEmpty(qty))
            {
                coefficient = "0%";
            }
            else
            {
                coefficient = Math.Round((Convert.ToDecimal(multiple) / Convert.ToDecimal(qty) * 100), 2).ToString() + "%";
            }
            if (coefficient != textBox4.Text)
            {
                MessageBox.Show("Purchase quality coefficient data is abnormal!");
                return;
            }

            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task_no);
                Dictionary<string, string> tabHead = new Dictionary<string, string>();
                tabHead.Add("pl_qty", label22.Text);
                tabHead.Add("supplier", label23.Text);
                tabHead.Add("approver", textBox1.Text);
                tabHead.Add("tabulator", textBox2.Text);
                tabHead.Add("pur_qty_cft", textBox4.Text);
                tabHead.Add("avg_use_rate", textBox5.Text);
                tabHead.Add("assessment", richTextBox1.Text);
                tabHead.Add("area_diff_cft", textBox3.Text);
                data.Add("tabHead", tabHead);
                DataTable dt1 = GetDgvToTable(dataGridView1);
                data.Add("qcm_hp_task_satra_l", dt1);

                DataTable dt2 = GetDgvToTable(dataGridView2);
                data.Add("qcm_hp_task_satra_r", dt2);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.BDM_Painted_Skin", "Painted_Skin_Report_Edit", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {

                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    GetPainted_Skin_Report_satra_l();
                    GetPainted_Skin_Report_Head();
                    GetPainted_Skin_Report_task_d();
                    HPPrint();
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
        /// 画皮打印
        /// </summary>
        public void HPPrint()
        {
            Dictionary<string, object> rdlcParam = new Dictionary<string, object>();
            rdlcParam.Add("item_no",label10.Text.Trim());
            rdlcParam.Add("date", label14.Text.Trim());
            rdlcParam.Add("item_name", label11.Text.Trim());
            rdlcParam.Add("supplier", label23.Text.Trim());
            rdlcParam.Add("qty", label12.Text.Trim());
            rdlcParam.Add("ITEM_TYPE_NAME", label16.Text.Trim());
            rdlcParam.Add("mtl_qty", label22.Text.Trim());
            rdlcParam.Add("BuyQualityCoefficient", textBox4.Text.Trim());
            rdlcParam.Add("AverageUsage", textBox5.Text.Trim());
            rdlcParam.Add("assessment", richTextBox1.Text.Trim());
            rdlcParam.Add("area_diff_cft", textBox3.Text.Trim());

            rdlcParam.Add("areadt", GetDgvToTable(dataGridView1));
            rdlcParam.Add("leveldt", GetDgvToTable(dataGridView2));
            using (HPPrint h=new HPPrint(rdlcParam))
            {
                h.ShowDialog();
            }
        }
    }
}
