using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_AQL.AQL_FrmBase;
using SJeMES_Control_Library;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.WebAPI;
using SjeMES_QCM_Ex;
using SJeMES_Report.AQL;
using SJeMES_Report.QCM_EX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SJeMES_AQL.Common.Enum;

namespace SJeMES_AQL
{
    public partial class F_AQL_CheckthedataMain1 : Form
    {
        public Dictionary<string, object> dics = new Dictionary<string, object>();
        /// <summary>
        /// 表格线的误差值
        /// </summary>
        private int hreadcoxin = 10;
        /// <summary>
        /// 传参变量（记录那一边的操作）
        /// </summary>
        public  Dictionary<string, object> ref_data = new Dictionary<string, object>();
      
        public F_AQL_CheckthedataMain1(Dictionary<string,object> _dic)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            dics = _dic;
            DisabledEdit();
        }

        public void DisabledEdit()
        {
            if (dics["effective_status"].ToString() == "失效")
            {
                button1.Enabled = false;
            }
        }

        public string autograph_code_a = "";
        public string autograph_code_b = "";
        public string autograph_code_c = "";
        public string autograph_code_d = "";
        public string autograph_code_e = "";
        public string curr_login_user_code = "";
        public string curr_login_user_name = "";

        //样式
        private static void SryleDell(DataGridView dgv)
        {
            //禁止拖动列
            dgv.AllowUserToResizeColumns = false;
            //自适应
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //内容颜色
            dgv.DefaultCellStyle.ForeColor = Color.Blue;
            //表头样式,
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 12f, FontStyle.Bold);
            //行内容样式
            dgv.RowsDefaultCellStyle.Font = new Font("宋体", 12f);
           dgv.DefaultCellStyle.SelectionBackColor = Color.White;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Blue;
        }
        private void F_AQL_CheckthedataMain1_Load(object sender, EventArgs e)
        {
         /*   this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;*/
            SryleDell(dataGridView1);
            SryleDell(dataGridView2);
            SryleDell(dataGridView3);
            SryleDell(dataGridView4);
            SryleDell(dataGridView5);
            GetDataView();
            ref_data = new Dictionary<string, object>();
            ref_data.Add(BotAtype.typekey, "");
            ref_data.Add(BotAtype.typekey1, "");
            ref_data.Add(BotAtype.typekey2, "");
            ref_data.Add(BotAtype.typekey3, "");
            ref_data.Add(BotAtype.typekey4, "");
            ref_data.Add(BotAtype.typekey5, "");
            ref_data.Add(BotAtype.typekey6, "");
        }
        public void GetDataView()
        {
            try
            {

                if (dics.Keys.Count > 0)
                {
                    textBox1.Text = dics["po"].ToString();//po
                    textBox3.Text = dics["num"].ToString();//po数量
                    textBox6.Text = dics["shoe_name"].ToString();//鞋型
                    textBox7.Text = dics["guojia"].ToString();//客户
                    textBox2.Text = "";//确认
                    textBox4.Text = dics["vas"].ToString();//vas
                    textBox5.Text = dics["art"].ToString();
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                string putin_date = string.Empty;
                //键值对传值
                data.Add("task_no", dics["task_no"].ToString());//task_no
                data.Add("po", dics["po"].ToString());//po
                data.Add("art", dics["art"].ToString());//art_no
                data.Add("shoe_no", dics["shoe_no"].ToString());//鞋型编号
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_Checkthedata1",//类名
                                            "Get_Main",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable data1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data1"].ToString());
                DataTable data2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data2"].ToString());
                DataTable data3 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data3"].ToString());
                DataTable data3a = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data3a"].ToString());
                DataTable data4 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data4"].ToString());
                DataTable data5 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data5"].ToString());
                this.curr_login_user_code = dic["user_code"].ToString();
                this.curr_login_user_name = dic["user_name"].ToString();

                //验货文件查询
                dataGridView1.Rows.Clear();
                if (data1.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in data1.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["Columna1"].Value = "view proof";//验货证明 dr["yhzm"].ToString()
                        dgvr.Cells["Columna2"].Value = "formal order";//正式订单
                        dgvr.Cells["Columna3"].Value = "check the details";//订单包装材料预算
                        //dgvr.Cells["Columna4"].Value = dr[""].ToString();//分段资料
                        dgvr.Cells["Columna5"].Value = "Special Packaging Information";//特殊包装资料
                        dgvr.Cells["Columna6"].Value = "sample sheet";//样品单
                        Rectangle R = dataGridView1.GetCellDisplayRectangle(dgvr.Cells["Columna7"].ColumnIndex, dgvr.Cells["Columna7"].RowIndex, false); //获取单元格位置
                        panela1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位panel2.中间有坐标位置的转换 
                        panela1.Visible = true;
                        i++;
                    }
                }
                dataGridView1.ClearSelection();

                //测试结果查询
                dataGridView2.Rows.Clear();
                if (data2.Rows.Count > 0)
                {
                    int i = 0;
                    int cindex = 0;
                    int rindex = 0;
                    foreach (DataRow dr in data2.Rows)
                    {
                        dataGridView2.Rows.Add();
                        DataGridViewRow dgvr = dataGridView2.Rows[i];
                        dgvr.Cells["Columnb1"].Value = "view report";//A-01报告//查看报告
                        dgvr.Cells["Columnb2"].Value = "check the details";//FGT报告//查看详情
                        dgvr.Cells["Columnb2a"].Value = dr["TASK_NO"].ToString();//FGT报告
                        dgvr.Cells["Columnb3"].Value = "check the details";//拉力测试结果//查看详情
                        dgvr.Cells["Columnb3a"].Value = dr["TASK_NO2"].ToString();//拉力测试结果
                        if (i == 0)
                        {
                            cindex = dgvr.Cells["Columnb4"].ColumnIndex;
                            rindex = dgvr.Cells["Columnb4"].RowIndex;
                        }
                        Rectangle R = dataGridView2.GetCellDisplayRectangle(cindex, rindex, false); //获取单元格位置 2/0
                        panelb1.SetBounds(R.X + dataGridView2.Location.X, R.Y + dataGridView2.Location.Y, R.Width, dataGridView2.Height - dataGridView2.ColumnHeadersHeight - hreadcoxin);
                        panelb1.Visible = true;

                        i++;
                    }
                }
                dataGridView2.ClearSelection();

                //产品安全测试报告v1
                dataGridView3.Rows.Clear();

                if (data3.Rows.Count>0 || data3a.Rows.Count>0)
                {
                   
                    int i = 0;
                    int cindex = 0;
                    int rindex = 0;
                    if (data3a.Rows.Count > data3.Rows.Count)
                    {
                        foreach (DataRow dr in data3a.Rows)
                        {
                            dataGridView3.Rows.Add();
                            DataGridViewRow dgvr = dataGridView3.Rows[i];
                            foreach (DataRow dr2 in data3.Rows)
                            {
                                dgvr.Cells["Columnc1"].Value = dr2["enum_value"].ToString();
                                dgvr.Cells["Columnc1v"].Value = dr2["file_url"].ToString();
                                data3.Rows.Remove(dr2);
                                break;

                            }
                            
                            dgvr.Cells["Columnc2"].Value = dr["enum_value"].ToString();
                            dgvr.Cells["Columnc2v"].Value = dr["file_url"].ToString();

                            if (i == 0)
                            {
                                cindex = dgvr.Cells["Columnc3"].ColumnIndex;
                                rindex = dgvr.Cells["Columnc3"].RowIndex;
                            }
                            i++;
                        }
                        Rectangle R = dataGridView3.GetCellDisplayRectangle(cindex, rindex, false); //获取单元格位置 2/0
                        panelc1.SetBounds(R.X + dataGridView3.Location.X, R.Y + dataGridView3.Location.Y, R.Width, dataGridView3.Height - dataGridView3.ColumnHeadersHeight - hreadcoxin);
                        panelc1.Visible = true;
                    }
                    else
                    {
                        foreach (DataRow dr in data3.Rows)
                        {
                            dataGridView3.Rows.Add();
                            DataGridViewRow dgvr = dataGridView3.Rows[i];
                            foreach (DataRow dr2 in data3a.Rows)
                            {
                                dgvr.Cells["Columnc2"].Value = dr2["enum_value"].ToString();
                                dgvr.Cells["Columnc2v"].Value = dr2["file_url"].ToString();
                                data3a.Rows.Remove(dr2);
                                break;

                            }
                            dgvr.Cells["Columnc1"].Value = dr["enum_value"].ToString();
                            dgvr.Cells["Columnc1v"].Value = dr["file_url"].ToString();
                            if (i == 0)
                            {
                                cindex = dgvr.Cells["Columnc3"].ColumnIndex;
                                rindex = dgvr.Cells["Columnc3"].RowIndex;
                            }

                            i++;
                        }
                        Rectangle R = dataGridView3.GetCellDisplayRectangle(cindex, rindex, false); //获取单元格位置 2/0
                        panelc1.SetBounds(R.X + dataGridView3.Location.X, R.Y + dataGridView3.Location.Y, R.Width, dataGridView3.Height - dataGridView3.ColumnHeadersHeight - hreadcoxin);
                        panelc1.Visible = true;
                    }
                   
                }
                dataGridView3.ClearSelection();

                //产品安全测试报告v2
                dataGridView4.Rows.Clear();
                if (data4.Rows.Count > 0)
                {
                    
                    int i = 0;
                    int cindex = 0;
                    int rindex = 0;
                    foreach (DataRow dr in data4.Rows)
                    {
                        dataGridView4.Rows.Add();
                        DataGridViewRow dgvr = dataGridView4.Rows[i];
                        dgvr.Cells["Columnd1"].Value = dr["file_name"].ToString();//文件名称
                        dgvr.Cells["Columnd2"].Value = dr["valid_time"].ToString();//有效时间
                        dgvr.Cells["Columnd4"].Value = dr["file_url"].ToString();//文件路径
                        
                        if (i == 0)
                        {
                            cindex = dgvr.Cells["Columnd3"].ColumnIndex;
                            rindex = dgvr.Cells["Columnd3"].RowIndex;
                        }
                       
                        i++;
                    }
                    Rectangle R = dataGridView4.GetCellDisplayRectangle(cindex, rindex, false); //获取单元格位置 2/0
                    paneld1.SetBounds(R.X + dataGridView4.Location.X, R.Y + dataGridView4.Location.Y, R.Width, dataGridView4.Height- dataGridView4.ColumnHeadersHeight- hreadcoxin);
                    paneld1.Visible = true;
                }
                dataGridView4.ClearSelection();

                //PD/VS结果查询
                dataGridView5.Rows.Clear();
                if (data5.Rows.Count > 0)
                {
                    int i = 0;
                    int cindex = 0;
                    int rindex = 0;
                    foreach (DataRow dr in data5.Rows)
                    {
                        dataGridView5.Rows.Add();
                        DataGridViewRow dgvr = dataGridView5.Rows[i];
                        dgvr.Cells["Columne1"].Value = dr["file_name"].ToString();
                        dgvr.Cells["Columne2"].Value = dr["file_url"].ToString();
                        if (i == 0)
                        {
                            cindex = dgvr.Cells["Columne3"].ColumnIndex;
                            rindex = dgvr.Cells["Columne3"].RowIndex;
                        }

                        i++;
                    }
                    Rectangle R = dataGridView5.GetCellDisplayRectangle(cindex, rindex, false); //获取单元格位置 2/0
                    panele1.SetBounds(R.X + dataGridView5.Location.X, R.Y + dataGridView5.Location.Y, R.Width, dataGridView5.Height - dataGridView5.ColumnHeadersHeight - hreadcoxin);
                    panele1.Visible = true;
                }
                dataGridView5.ClearSelection();


                DataTable cheked_list = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["cheked_list"].ToString());
                dvgdata = cheked_list;//(用于接收数据展示小窗体勾选内容)
                if (cheked_list.Rows.Count > 0)//为单选框赋值
                {
                    //0：验货文件 1：成品鞋测试报告 2：产品安全测试报告CPSIA 3：产品安全测试报告文件 4：FD/VS 结果查询
                    //0：验货证明 1：正式订单 2：订单包装材料预算 3：特殊包装 4：样品单<>
                    //5：A-01报告 6：FGT报告 7：拉力测试结果 <>
                    //8：CPSIA 9：vegan<>
                    //10：客户国家特殊要求
                    //11：FD/VS
                    //12：MCS 13：SHAS 14：量产 15：仓库 16：CMA合格 17：UV-C处理 18：防霉包装纸 19：特殊的外观标准 20：工厂免责声明 21：FIT 22：防霉
                    foreach (DataRow item in cheked_list.Rows)
                    {
                        switch (item["conclusion_type"].ToString())
                        {
                            case Atype.typekey:
                                switch (item["conclusion"].ToString())
                                {
                                    case "0":
                                        radioButtona1.Checked = true;
                                        break;
                                    case "1":
                                        radioButtona2.Checked = true;
                                        break;
                                }
                                if (item["is_autograph"].ToString()== "1")
                                {
                                    checkBoxa1.Checked = true;
                                    autograph_code_a = item["autograph"].ToString();
                                    lbl_a.Text = item["STAFF_NAME"].ToString();
                                }
                                else
                                {
                                    checkBoxa1.Checked = false;
                                }
                                break;
                            case Atype.typekey5:
                                switch (item["conclusion"].ToString())
                                {
                                    case "0":
                                        radioButtonb1.Checked = true;
                                        break;
                                    case "1":
                                        radioButtonb2.Checked = true;
                                        break;
                                }
                                if (item["is_autograph"].ToString() == "1")
                                {
                                    checkBoxb1.Checked = true;
                                    autograph_code_b = item["autograph"].ToString();
                                    lbl_b.Text = item["STAFF_NAME"].ToString();
                                }
                                else
                                {
                                    checkBoxb1.Checked = false;
                                }
                                break;
                            case Atype.typekey8:
                                switch (item["conclusion"].ToString())
                                {
                                    case "0":
                                        radioButtonc1.Checked = true;
                                        break;
                                    case "1":
                                        radioButtonc2.Checked = true;
                                        break;
                                }
                                if (item["is_autograph"].ToString() == "1")
                                {
                                    checkBoxc1.Checked = true;
                                    autograph_code_c = item["autograph"].ToString();
                                    lbl_c.Text = item["STAFF_NAME"].ToString();
                                }
                                else
                                {
                                    checkBoxc1.Checked = false;
                                }
                              
                                break;
                            case Atype.typekey10:
                                switch (item["conclusion"].ToString())
                                {
                                    case "0":
                                        radioButtond1.Checked = true;
                                        break;
                                    case "1":
                                        radioButtond2.Checked = true;
                                        break;
                                }
                                if (item["is_autograph"].ToString() == "1")
                                {
                                    checkBoxd1.Checked = true;
                                    autograph_code_d = item["autograph"].ToString();
                                    lbl_d.Text = item["STAFF_NAME"].ToString();
                                }
                                else
                                {
                                    checkBoxd1.Checked = false;
                                }
                              
                                break;
                            case Atype.typekey11:
                                switch (item["conclusion"].ToString())
                                {
                                    case "0":
                                        radioButtone1.Checked = true;
                                        break;
                                    case "1":
                                        radioButtone2.Checked = true;
                                        break;
                                }
                                if (item["is_autograph"].ToString() == "1")
                                {
                                    checkBoxe1.Checked = true;
                                    autograph_code_e = item["autograph"].ToString();
                                    lbl_e.Text = item["STAFF_NAME"].ToString();
                                }
                                else
                                {
                                    checkBoxe1.Checked = false;
                                }
                               
                                break;

                        }
                       
                    }
                }
                data_remark = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data_remark"].ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to submit?!", "submit", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dr == DialogResult.OK)
            {
                try
                {
                    //0：验货文件 1：成品鞋测试报告 2：产品安全测试报告CPSIA 3：产品安全测试报告文件 4：FD/VS 结果查询
                    //0：未核对 1：已核对 2：签名确认
                    List<Dictionary<string, object>> dic_iist = new List<Dictionary<string, object>>();
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    bool falg = false;
                    #region Inspection documents

                    if (radioButtona1.Checked)
                    {
                        dic.Add("conclusion", "0");
                        falg = true;
                    }
                    else if (radioButtona2.Checked)
                    {
                        dic.Add("conclusion", "1");
                        falg = true;
                    }
                    else
                    {
                        dic.Add("conclusion", "");
                    }
                    if (checkBoxa1.Checked)
                    {
                        dic.Add("is_autograph", "1");
                    }
                    else
                    {
                        dic.Add("is_autograph", "0");
                    }
                    dic.Add("user_code", autograph_code_a);
                    //if (falg)
                    //{
                        dic.Add("conclusion_type", "0");
                        dic_iist.Add(dic);
                    //}

                    #endregion

                    #region Finished shoe test report
                    dic = new Dictionary<string, object>();
                    falg = false;
                    if (radioButtonb1.Checked)
                    {
                        dic.Add("conclusion", "0");
                        falg = true;
                    }
                    else if (radioButtonb2.Checked)
                    {
                        dic.Add("conclusion", "1");
                        falg = true;
                    }
                    else
                    {
                        dic.Add("conclusion", "");
                    }
                    if (checkBoxb1.Checked)
                    {
                        dic.Add("is_autograph", "1");
                    }
                    else
                    {
                        dic.Add("is_autograph", "0");
                    }
                    dic.Add("user_code", autograph_code_b);
                    //if (falg)
                    //{
                        dic.Add("conclusion_type", "1");
                        dic_iist.Add(dic);
                    //}

                    #endregion

                    #region Product Safety Testing Report (CPSIA) 
                    dic = new Dictionary<string, object>();
                    falg = false;
                    if (radioButtonc1.Checked)
                    {
                        dic.Add("conclusion", "0");
                        falg = true;
                    }
                    else if (radioButtonc2.Checked)
                    {
                        dic.Add("conclusion", "1");
                        falg = true;
                    }
                    else
                    {
                        dic.Add("conclusion", "");
                    }
                    if (checkBoxc1.Checked)
                    {
                        dic.Add("is_autograph", "1");
                    }
                    else
                    {
                        dic.Add("is_autograph", "0");
                    }
                    dic.Add("user_code", autograph_code_c);
                    //if (falg)
                    //{
                        dic.Add("conclusion_type", "2");
                        dic_iist.Add(dic);
                    //}

                    #endregion

                    #region Product safety test report document 
                    dic = new Dictionary<string, object>();
                    falg = false;
                    if (radioButtond1.Checked)
                    {
                        dic.Add("conclusion", "0");
                        falg = true;
                    }
                    else if (radioButtond2.Checked)
                    {
                        dic.Add("conclusion", "1");
                        falg = true;
                    }
                    else
                    {
                        dic.Add("conclusion", "");
                    }
                    if (checkBoxd1.Checked)
                    {
                        dic.Add("is_autograph", "1");

                    }
                    else
                    {
                        dic.Add("is_autograph", "0");
                    }
                    dic.Add("user_code", autograph_code_d);
                    //if (falg)
                    //{
                        dic.Add("conclusion_type", "3");
                        dic_iist.Add(dic);
                    //}

                    #endregion

                    #region FD/VS Result Inquiry
                    dic = new Dictionary<string, object>();
                    falg = false;
                    if (radioButtone1.Checked)
                    {
                        dic.Add("conclusion", "0");
                        falg = true;
                    }
                    else if (radioButtone2.Checked)
                    {
                        dic.Add("conclusion", "1");
                        falg = true;
                    }
                    else
                    {
                        dic.Add("conclusion", "");
                    }
                    if (checkBoxe1.Checked)
                    {
                        dic.Add("is_autograph", "1");

                    }
                    else
                    {
                        dic.Add("is_autograph", "0");
                    }
                    dic.Add("user_code", autograph_code_e);
                    //if (falg)
                    //{
                        dic.Add("conclusion_type", "4");
                        dic_iist.Add(dic);
                    //}
                    #endregion
                    if (dic_iist.Count < 1)
                    {
                        MessageBox.Show("Please complete the conclusion before proceeding with this operation");
                        return;
                    }
                    if (userCrlDic.Count > 0)
                    {
                        ((F_AQL_ShowFrm)userCrlDic["F_AQL_ShowFrm"]).getdata();
                        ((F_AQL_ShowFrm1)userCrlDic["F_AQL_ShowFrm1"]).getdata();
                        ((F_AQL_ShowFrm2)userCrlDic["F_AQL_ShowFrm2"]).getdata();
                        ((F_AQL_ShowFrm3)userCrlDic["F_AQL_ShowFrm3"]).getdata();
                        ((F_AQL_ShowFrm4)userCrlDic["F_AQL_ShowFrm4"]).getdata();
                        ((F_AQL_ShowFrm5)userCrlDic["F_AQL_ShowFrm5"]).getdata();
                    }


                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("dic_iist", dic_iist);
                    p.Add("task_no", dics["task_no"].ToString());
                    p.Add("po", dics["po"].ToString());
                    if (!flowLayoutPanel1.Visible)
                    {
                        var dicList = ref_data.Keys.ToList();
                        foreach (var item in dicList)
                        {
                            ref_data[item] = "";
                        }
                    }
                    p.Add("dic_data", ref_data);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                "SJ_AQLAPI", "SJ_AQLAPI.AQL_Checkthedata1", "Commit_data", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                    if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                    {
                        MessageBox.Show("Saved_Successfully");//保存成功
                    }
                    else
                        throw new Exception(j["ErrMsg"].ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Save_Failed，Reason：", ex.Message);//保存失败
                }
            }

        }

        //获取初始高度
        private int size = 0;
        //设置高度
        private int maxsize = 0;
        //获取仅支持的最大高度
        private int maxheight = 0;
        //只加载一次窗体
        private int flag = 0;
        private void frmshows(string frmType,UserControl frm)
        {
            maxsize += frm.Height+flowLayoutPanel1.Height- 2*hreadcoxin;
            this.flowLayoutPanel1.Controls.Add(frm);
            userCrlDic.Add(frmType, frm);
        }
        private DataTable dvgdata = new DataTable();
        /// <summary>
        /// 用于接收类型备注
        /// </summary>
        private DataTable data_remark=new DataTable();
        /// <summary>
        /// 小窗体内容
        /// </summary>
        /// <returns></returns>
        private void KeyValues()
        {
            //0：验货证明 1：正式订单 2：订单包装材料预算 3：特殊包装 4：样品单 5：A-01报告 6：FGT报告 7：拉力测试结果 8：CPSIA 9：vegan 10：客户国家特殊要求 11：FD/VS 12：MCS
            //
            //
            //
            //pivot88项目核对的
            //13：SHAS 14：量产 15：仓库 16：CMA合格 17：UV-C处理 18：防霉包装纸 19：特殊的外观标准 20：工厂免责声明 21：FIT 22：防霉

            //name(列名)
            //status(勾选条件)
            //type(类型)

            #region 基本要求
            List<Dictionary<string, object>> dic_ref_list = new List<Dictionary<string, object>>();
            string status = string.Empty;
            Dictionary<string, object> dic_ref = new Dictionary<string, object>();
            dic_ref.Add("name", Atype.typekeyname12);
            dic_ref.Add("type", Atype.typekey12);
            DataRow[] dr = null;
            int i = 0;
            if (dvgdata.Rows.Count > 0)
            {
                i = 1;
                dr = dvgdata.Select($@"task_no='{dics["task_no"]}' and conclusion_type='{Atype.typekey12}'");
                if (dr.Length > 0)
                {
                    status = dr[0]["CONCLUSION"].ToString();
                }
            }
            dic_ref.Add("status",status);
            dic_ref_list.Add(dic_ref);
            
            dic_ref = new Dictionary<string, object>();
            dic_ref.Add("name", Atype.typekeyname13);
            dic_ref.Add("type", Atype.typekey13);
            if (i== 1)
            {
                dr = dvgdata.Select($@"task_no='{dics["task_no"]}' and conclusion_type='{Atype.typekey13}'");
                if (dr.Length > 0)
                {
                    status = dr[0]["CONCLUSION"].ToString();
                }
            }
            dic_ref.Add("status", status);
            dic_ref_list.Add(dic_ref);

            dic_ref = new Dictionary<string, object>();
            dic_ref.Add("name", Atype.typekeyname5);
            dic_ref.Add("type", Atype.typekey5);
            if (i == 1)
            {
                dr = dvgdata.Select($@"task_no='{dics["task_no"]}' and conclusion_type='{Atype.typekey5}'");
                if (dr.Length > 0)
                {
                    status = dr[0]["CONCLUSION"].ToString();
                }
            }
            dic_ref.Add("status", status);
            dic_ref_list.Add(dic_ref);

            dic_ref = new Dictionary<string, object>();
            dic_ref.Add("name", Atype.typekeyname8);
            dic_ref.Add("type", Atype.typekey8);
            status = string.Empty;
            if (i == 1)
            {
                dr = dvgdata.Select($@"task_no='{dics["task_no"]}' and conclusion_type='{Atype.typekey8}'");
                if (dr.Length > 0)
                {
                    status = dr[0]["CONCLUSION"].ToString();
                }
            }
            //if (radioButtonc1.Checked) status = "0";
            //else if (radioButtonc2.Checked) status = "1";
            dic_ref.Add("status", status);
            dic_ref_list.Add(dic_ref);

            dic_ref = new Dictionary<string, object>();
            dic_ref.Add("name", Atype.typekeyname10);
            dic_ref.Add("type", Atype.typekey10);
            status = string.Empty;
            if (i == 1)
            {
                dr = dvgdata.Select($@"task_no='{dics["task_no"]}' and conclusion_type='{Atype.typekey10}'");
                if (dr.Length > 0)
                {
                    status = dr[0]["CONCLUSION"].ToString();
                }
            }
            //if (radioButtond1.Checked) status = "0";
            //else if (radioButtond2.Checked) status = "1";
            dic_ref.Add("status", status);
            dic_ref_list.Add(dic_ref); 
            
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("inputdata", dic_ref_list);
            dic.Add("returndata","");
            dic.Add("remark", "");
            if (data_remark.Rows.Count > 0)
            {
                dr = data_remark.Select($@"remark_type='{BotAtype.typekey}'");
                if (dr.Length > 0)
                {
                    dic["remark"] = dr[0]["remark"].ToString();
                }
            }
            
          
            ref_data[BotAtype.typekey] = dic;
            #endregion

            #region 金属探测要求
            dic_ref_list = new List<Dictionary<string, object>>();
            status = string.Empty;
            dic_ref = new Dictionary<string, object>();
            dic_ref.Add("name", Atype.typekeyname14);
            dic_ref.Add("type", Atype.typekey14);
            if (i == 1)
            {
                dr = dvgdata.Select($@"task_no='{dics["task_no"]}' and conclusion_type='{Atype.typekey14}'");
                if (dr.Length > 0)
                {
                    status = dr[0]["CONCLUSION"].ToString();
                }
            }
            dic_ref.Add("status", status);
            dic_ref_list.Add(dic_ref);

            dic_ref = new Dictionary<string, object>();
            dic_ref.Add("name", Atype.typekeyname15);
            dic_ref.Add("type", Atype.typekey15);
            if (i == 1)
            {
                dr = dvgdata.Select($@"task_no='{dics["task_no"]}' and conclusion_type='{Atype.typekey15}'");
                if (dr.Length > 0)
                {
                    status = dr[0]["CONCLUSION"].ToString();
                }
            }
            dic_ref.Add("status", status);
            dic_ref_list.Add(dic_ref);


            dic = new Dictionary<string, object>();
            dic.Add("inputdata", dic_ref_list);
            dic.Add("returndata", "");
            dic.Add("remark", "");
            if (data_remark.Rows.Count > 0)
            {
                dr = data_remark.Select($@"remark_type='{BotAtype.typekey1}'");
                if (dr.Length > 0)
                {
                    dic["remark"] = dr[0]["remark"].ToString();
                }
            }

            ref_data[BotAtype.typekey1] = dic;
            #endregion

            #region FGT
            dic_ref_list = new List<Dictionary<string, object>>();
            status = string.Empty;
            dic_ref = new Dictionary<string, object>();
            dic_ref.Add("name", Atype.typekeyname6);
            dic_ref.Add("type", Atype.typekey6);
            if (i == 1)
            {
                dr = dvgdata.Select($@"task_no='{dics["task_no"]}' and conclusion_type='{Atype.typekey6}'");
                if (dr.Length > 0)
                {
                    status = dr[0]["CONCLUSION"].ToString();
                }
            }
            dic_ref.Add("status", status);
            dic_ref_list.Add(dic_ref);

            dic_ref = new Dictionary<string, object>();
            dic_ref.Add("name", Atype.typekeyname16);
            dic_ref.Add("type", Atype.typekey16);
            if (i == 1)
            {
                dr = dvgdata.Select($@"task_no='{dics["task_no"]}' and conclusion_type='{Atype.typekey16}'");
                if (dr.Length > 0)
                {
                    status = dr[0]["CONCLUSION"].ToString();
                }
            }
            dic_ref.Add("status", status);
            dic_ref_list.Add(dic_ref);


            dic = new Dictionary<string, object>();
            dic.Add("inputdata", dic_ref_list);
            dic.Add("remark", "");
            if (data_remark.Rows.Count > 0)
            {
                dr = data_remark.Select($@"remark_type='{BotAtype.typekey2}'");
                if (dr.Length > 0)
                {
                    dic["remark"] = dr[0]["remark"].ToString();
                }
            }

            ref_data[BotAtype.typekey2] = dic;
            #endregion

            #region 防雾
            dic_ref_list = new List<Dictionary<string, object>>();
            status = string.Empty;
            dic_ref = new Dictionary<string, object>();
            dic_ref.Add("name", Atype.typekeyname17);
            dic_ref.Add("type", Atype.typekey17);
            if (i == 1)
            {
                dr = dvgdata.Select($@"task_no='{dics["task_no"]}' and conclusion_type='{Atype.typekey17}'");
                if (dr.Length > 0)
                {
                    status = dr[0]["CONCLUSION"].ToString();
                }
            }
            dic_ref.Add("status", status);
            dic_ref_list.Add(dic_ref);

            dic_ref = new Dictionary<string, object>();
            dic_ref.Add("name", Atype.typekeyname18);
            dic_ref.Add("type", Atype.typekey18);
            if (i == 1)
            {
                dr = dvgdata.Select($@"task_no='{dics["task_no"]}' and conclusion_type='{Atype.typekey18}'");
                if (dr.Length > 0)
                {
                    status = dr[0]["CONCLUSION"].ToString();
                }
            }
            dic_ref.Add("status", status);
            dic_ref_list.Add(dic_ref);


            dic = new Dictionary<string, object>();
            dic.Add("inputdata", dic_ref_list);
            dic.Add("returndata", "");
            dic.Add("remark", "");
            if (data_remark.Rows.Count > 0)
            {
                dr = data_remark.Select($@"remark_type='{BotAtype.typekey3}'");
                if (dr.Length > 0)
                {
                    dic["remark"] = dr[0]["remark"].ToString();
                }
            }

            ref_data[BotAtype.typekey3] = dic;
            #endregion

            #region 特例管理
            dic_ref_list = new List<Dictionary<string, object>>();
            status = string.Empty;
            dic_ref = new Dictionary<string, object>();
            dic_ref.Add("name", Atype.typekeyname19);
            dic_ref.Add("type", Atype.typekey19);
            if (i == 1)
            {
                dr = dvgdata.Select($@"task_no='{dics["task_no"]}' and conclusion_type='{Atype.typekey19}'");
                if (dr.Length > 0)
                {
                    status = dr[0]["CONCLUSION"].ToString();
                }
            }
            dic_ref.Add("status", status);
            dic_ref_list.Add(dic_ref);

            dic_ref = new Dictionary<string, object>();
            dic_ref.Add("name", Atype.typekeyname20);
            dic_ref.Add("type", Atype.typekey20);
            if (i == 1)
            {
                dr = dvgdata.Select($@"task_no='{dics["task_no"]}' and conclusion_type='{Atype.typekey20}'");
                if (dr.Length > 0)
                {
                    status = dr[0]["CONCLUSION"].ToString();
                }
            }
            dic_ref.Add("status", status);
            dic_ref_list.Add(dic_ref);


            dic = new Dictionary<string, object>();
            dic.Add("inputdata", dic_ref_list);
            dic.Add("returndata", "");
            dic.Add("remark", "");
            if (data_remark.Rows.Count > 0)
            {
                dr = data_remark.Select($@"remark_type='{BotAtype.typekey4}'");
                if (dr.Length > 0)
                {
                    dic["remark"] = dr[0]["remark"].ToString();
                }
            }

            ref_data[BotAtype.typekey4] = dic;
            #endregion

            #region 检查清单v0
            dic_ref_list = new List<Dictionary<string, object>>();
            status = string.Empty;
            dic_ref = new Dictionary<string, object>();
            dic_ref.Add("name", "One-pedal try-on is qualified (tool inspection)");//一脚蹬试穿合格(工具检测)
            dic_ref.Add("type", Atype.typekey21);
            if (i == 1)
            {
                dr = dvgdata.Select($@"task_no='{dics["task_no"]}' and conclusion_type='{Atype.typekey21}'");
                if (dr.Length > 0)
                {
                    status = dr[0]["CONCLUSION"].ToString();
                }
            }
            dic_ref.Add("status", status);
            dic_ref_list.Add(dic_ref);

            dic = new Dictionary<string, object>();
            dic.Add("inputdata", dic_ref_list);
            dic.Add("returndata", "");
            dic.Add("remark", "");
            if (data_remark.Rows.Count > 0)
            {
                dr = data_remark.Select($@"remark_type='{BotAtype.typekey5}'");
                if (dr.Length > 0)
                {
                    dic["remark"] = dr[0]["remark"].ToString();
                }
            }

            ref_data[BotAtype.typekey5] = dic;
            #endregion

            #region 检查清单v1
            dic_ref_list = new List<Dictionary<string, object>>();
            status = string.Empty;
            dic_ref = new Dictionary<string, object>();
            dic_ref.Add("name", "Moisture content test passed");//含水量测试合格
            dic_ref.Add("type", Atype.typekey22);
            if (i == 1)
            {
                dr = dvgdata.Select($@"task_no='{dics["task_no"]}' and conclusion_type='{Atype.typekey22}'");
                if (dr.Length > 0)
                {
                    status = dr[0]["CONCLUSION"].ToString();
                }
            }
            dic_ref.Add("status", status);
            dic_ref_list.Add(dic_ref);

            dic = new Dictionary<string, object>();
            dic.Add("inputdata", dic_ref_list);
            dic.Add("returndata", "");
            dic.Add("remark", "");
            if (data_remark.Rows.Count > 0)
            {
                dr = data_remark.Select($@"remark_type='{BotAtype.typekey6}'");
                if (dr.Length > 0)
                {
                    dic["remark"] = dr[0]["remark"].ToString();
                }
            }

            ref_data[BotAtype.typekey6] = dic;


            #endregion
        }
        private Dictionary<string, UserControl> userCrlDic = new Dictionary<string, UserControl>();
        private void button2_Click(object sender, EventArgs e)
        {
            InitialFlowLayoutPanel1();
        }

        public void InitialFlowLayoutPanel1()
        {
            if (flag == 0)
            {
                userCrlDic.Clear();
                KeyValues();
                frmshows("F_AQL_ShowFrm", new F_AQL_ShowFrm(ref_data));//基本要求
                frmshows("F_AQL_ShowFrm1", new F_AQL_ShowFrm1(ref_data));//金属探测要求
                frmshows("F_AQL_ShowFrm2", new F_AQL_ShowFrm2(ref_data));//FGT
                frmshows("F_AQL_ShowFrm3", new F_AQL_ShowFrm3(ref_data));//防雾
                frmshows("F_AQL_ShowFrm4", new F_AQL_ShowFrm4(ref_data));//特例管理
                frmshows("F_AQL_ShowFrm5", new F_AQL_ShowFrm5(ref_data));
                flag = 1;
            }
            if (size == 0)
            {
                size = this.Height;
            }
            if (this.Height < (size + maxsize))
            {
                maxheight = this.Height;
                button2.Text = "Put_Away";//收起
                flowLayoutPanel1.Visible = true;
                this.Height += maxsize;
                this.flowLayoutPanel1.Height += maxsize;
                size -= maxsize;


            }
            else
            {
                button2.Text = "Open";//打开
                flowLayoutPanel1.Visible = false;
                this.Height = maxheight == 0 ? this.Height : maxheight;
                this.flowLayoutPanel1.Height -= maxsize;
                size += maxsize;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    switch (dataGridView1.Columns[e.ColumnIndex].Name)
                    {
                        
                        case "Columna1":
                            //                            string sql = $@"SELECT 
                            //ICNO AS ICNo,
                            //PROD_NAME AS ModelNo,
                            //ART_NO AS ArticleNo,
                            //OEA04 AS CustomeNo,
                            //SUM(TC_RVBS04) AS QuantilyNum,
                            //COUNTRY AS Destination
                            //FROM VW_BA_ORD WHERE OEA10='{dics["po"]}'
                            //GROUP BY ICNO,PROD_NAME,ART_NO,OEA04,COUNTRY";
                            string sql = $@"
SELECT
	PG_WMS.GF_SESEQ_ICNO(oi.ORG_ID,oi.SE_ID,oi.SE_SEQ,'')  AS ICNo,
	rp.NAME_T AS ModelNo,
	oi.PROD_NO AS ArticleNo,
LTRIM(om.se_custid,0)	  AS CustomeNo,
	oi.SE_QTY AS QuantilyNum,
	om.DESCOUNTRY_NAME AS Destination,
nvl(om.PO_AGGREGATOR,'/') as LineAggregator
FROM
	BDM_SE_ORDER_ITEM oi 
LEFT JOIN BDM_SE_ORDER_MASTER om ON om.ORG_ID=oi.ORG_ID AND om.SE_ID=oi.SE_ID
LEFT JOIN BDM_RD_PROD rp ON rp.PROD_NO=oi.PROD_NO
WHERE
	om.MER_PO = '{dics["po"]}' ";
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("sql", sql);
                            string retdata = WebAPIHelper.Post(
                                                      Program.Client.APIURL,
                                                      "SJ_BDMAPI",//类库名
                                                      "SJ_BDMAPI.BASE",//类名
                                                      "GetDataTable",//方法名
                                                      Program.Client.UserToken,//token
                                                      Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                            if (!ret.IsSuccess)
                            {
                                throw new Exception(ret.ErrMsg);
                            }
                            DataTable data = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                            string ICNo = string.Empty;
                            string ModelNo = string.Empty;
                            string ArticleNo = string.Empty;
                            string CustomeNo = string.Empty;
                            string PoNo = dics["po"].ToString();
                            string QuantilyNum = string.Empty;
                            string Destination = string.Empty;
                            string LineAggregator = string.Empty;// edit on 20240712(pochange)
                            if (data.Rows.Count < 1)
                            {

                                MessageBox.Show("The PO has no inspection report");
                                this.Close();
                            }
                            else
                            {
                                ICNo = data.Rows[0]["ICNo"].ToString();
                                ModelNo = data.Rows[0]["ModelNo"].ToString();
                                QuantilyNum = data.Rows[0]["QuantilyNum"].ToString();
                                ArticleNo = data.Rows[0]["ArticleNo"].ToString();
                                CustomeNo = data.Rows[0]["CustomeNo"].ToString();
                                Destination = data.Rows[0]["Destination"].ToString();
                                LineAggregator = data.Rows[0]["LineAggregator"].ToString();// edit on 20240712(pochange)
                            }
                            
//                            sql = $@"SELECT 
//AQL022 as  QC,substr(AQL024,0,8) as QCTIME, AQL021 as   MANAGER,substr(AQL023,0,8) as   MANAGERTIME
// FROM SJQDMS_POAQL
//WHERE AQL001='{dics["po"]}'";
//                            p = new Dictionary<string, object>();
//                            p.Add("sql", sql);
//                            retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
//                                                      Program.Client.APIURL,
//                                                      "SJ_BDMAPI",//类库名
//                                                      "SJ_BDMAPI.BASE_QDM",//类名
//                                                      "GetDataTable",//方法名
//                                                      Program.Client.UserToken,//token
//                                                      Newtonsoft.Json.JsonConvert.SerializeObject(p));
//                            ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
//                            if (!ret.IsSuccess)
//                            {
//                                throw new Exception(ret.ErrMsg);
//                            }
                            //data = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                            string qc = dics["customer_autograph"].ToString();
                            string qctime = dics["customer_autograph_date"].ToString();
                            string manager = dics["factory_autograph"].ToString();
                            string managertime = dics["factory_autograph_date"].ToString();
                            //if (data.Rows.Count > 0)
                            //{
                            //    qc = data.Rows[0]["QC"].ToString();
                            //    qctime = data.Rows[0]["QCTIME"].ToString();
                            //    manager = data.Rows[0]["MANAGER"].ToString();
                            //    managertime = data.Rows[0]["MANAGERTIME"].ToString();
                            //}
                           
                            string dateString = string.Empty;
                            string dateString2 = string.Empty;

                            if (!string.IsNullOrEmpty(qctime.Trim()))
                            {
                                DateTime dt = DateTime.ParseExact(qctime, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                                dateString = dt.ToString("yyyy/MM/dd");
                            }
                            if (!string.IsNullOrEmpty(managertime.Trim()))
                            {
                                DateTime dt2 = DateTime.ParseExact(managertime, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                                dateString2 = dt2.ToString("yyyy/MM/dd");
                            }
                           
//                            sql = $@"
//SELECT 
//AQL002 as checktype,AQL020
// FROM SJQDMS_POAQL
//WHERE AQL001='{dics["po"]}'";
//                            p = new Dictionary<string, object>();
//                            p.Add("sql", sql);
//                            retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
//                                                      Program.Client.APIURL,
//                                                      "SJ_BDMAPI",//类库名
//                                                      "SJ_BDMAPI.BASE_QDM",//类名
//                                                      "GetDataTable",//方法名
//                                                      Program.Client.UserToken,//token
//                                                      Newtonsoft.Json.JsonConvert.SerializeObject(p));
//                            ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
//                            if (!ret.IsSuccess)
//                            {
//                                throw new Exception(ret.ErrMsg);
//                            }
//                            data = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                            string checktype = string.Empty;
                            string result = string.Empty;
                            string result2 = string.Empty;
                            //if (data.Rows.Count > 0)
                            //{
                            //    checktype = data.Rows[0]["checktype"].ToString();
                            //    result = data.Rows[0]["AQL020"].ToString();
                            //}
                            result = "";
                            result2 = "√";
                            //if (Program.Client.UserCode == "")
                            //{
                            //    result = "";
                            //    result2 = "√";
                            //}
                            //else
                            //{
                            //    result = "√";
                            //    result2 = "";
                            //}
                            Dictionary<string, string> dicdata = new Dictionary<string, string>();
                            dicdata.Add("ICNo", ICNo);
                            dicdata.Add("ModelNo", ModelNo);
                            dicdata.Add("QuantilyNum", QuantilyNum);
                            dicdata.Add("ArticleNo", ArticleNo);
                            dicdata.Add("CustomeNo", CustomeNo);
                            dicdata.Add("Destination", Destination);
                            dicdata.Add("PoNo", dics["po"].ToString());
                            dicdata.Add("qc", qc);
                            dicdata.Add("qctime", qctime);
                            dicdata.Add("manager", manager);
                            dicdata.Add("managertime", managertime);
                            dicdata.Add("dateString", dateString);
                            dicdata.Add("dateString2", dateString2);
                            dicdata.Add("result", result);
                            dicdata.Add("result2", result2);
                            dicdata.Add("LineAggregator", LineAggregator);// edit on 20240712(pochange)
                            frmInspectionCertificate frm = new frmInspectionCertificate(dicdata);
                            frm.Show();

                            break;
                        case "Columna2"://正式订单
                            string po = dics["po"].ToString();
                            if (string.IsNullOrEmpty(po))
                            {
                                string tipsMsg = $@"The po number is empty and cannot be queried";//po号为空，无法查询
                                MessageBox.Show(tipsMsg);
                                return;
                            }
                            Dictionary<string, object> p_zsdd = new Dictionary<string, object>();
                            p_zsdd.Add("po", po);
                            string retdata_zsdd = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                    "SJ_AQLAPI",//类库名
                                                    "SJ_AQLAPI.AQL_OOrderFlie",//类名
                                                    "GetFileGuidByName",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p_zsdd));
                            ResultObject ret_zsdd = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata_zsdd);

                            if (!ret_zsdd.IsSuccess)
                            {
                                MessageBox.Show(ret_zsdd.ErrMsg);
                                return;
                            }
                            Dictionary<string, object> dic_zsdd = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret_zsdd.RetData);
                            ShowFileHelper.ShowFile(Program.Client.PicUrl + dic_zsdd["file_url"].ToString(), dic_zsdd["file_name"].ToString());
                            break;
                        case "Columna3":
                            //订单预算
                            Dictionary<string, object> p1 = new Dictionary<string, object>();
                            p1.Add("po", dics["po"].ToString());
                            string retdata1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                    "SJ_AQLAPI",//类库名
                                                    "SJ_AQLAPI.AQL_Checkthedata1",//类名
                                                    "GetUrlDDBZCL",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p1));
                            ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata1);

                            if (!ret1.IsSuccess)
                            {
                                throw new Exception(ret1.ErrMsg);
                            }
                            Dictionary<string,object> dic1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,object>>(ret1.RetData);
                            if (!string.IsNullOrEmpty(dic1["PDF_URL"].ToString()))
                            {
                                UploadFileResultDto moveRes = SJeMES_Framework.Common.HttpHelper.MoveNfsFile(Program.Client.UploadUrl, Program.Client.UserToken, Program.Client.CompanyCode, dic1["PDF_URL"].ToString());
                            }
                            FrmShowFile frm1 = new FrmShowFile(Program.Client.PicUrl.ToLower().Replace("api/commoncall", "") + dic1["PDF_URL"], "");
                            frm1.Show();
                            break;
                        case "Columna4":
                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add("po", dics["po"].ToString());
                            F_AQL_ASegmentinformation add = new F_AQL_ASegmentinformation(dic);
                            add.Show();
                            break;
                        case "Columna5"://特殊包装资料
                            string kehu = dics["kehu"].ToString();
                            string vas = dics["vas"].ToString();
                            if (string.IsNullOrEmpty(kehu) && string.IsNullOrEmpty(vas))
                            {
                                string tipsMsg = $@"客户号，VAS都为空，无法查询";
                                MessageBox.Show(tipsMsg);
                                return;
                            }
                            Dictionary<string, object> p_tsbz = new Dictionary<string, object>();
                            p_tsbz.Add("kehu", kehu);
                            p_tsbz.Add("vas", vas);
                            string retdata_tsbz = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                    "SJ_AQLAPI",//类库名
                                                    "SJ_AQLAPI.AQL_SpcPkgFile",//类名
                                                    "GetFileGuidByName",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p_tsbz));
                            ResultObject ret_tsbz = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata_tsbz);

                            if (!ret_tsbz.IsSuccess)
                            {
                                MessageBox.Show(ret_tsbz.ErrMsg);
                                return;
                            }
                            Dictionary<string, object> dic_tsbz = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret_tsbz.RetData);
                            ShowFileHelper.ShowFile(Program.Client.PicUrl + dic_tsbz["file_url"].ToString(), dic_tsbz["file_name"].ToString());
                            break;
                        case "Columna6"://样品单
                            string art_no = textBox5.Text;
                            F_AQL_Sample_List ypd_frm = new F_AQL_Sample_List(art_no);
                            ypd_frm.Show();
                            break;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    switch (dataGridView2.Columns[e.ColumnIndex].Name)
                    {
                        case "Columnb1":
                            string art = textBox5.Text;
                            string po = textBox1.Text;
                            string art_name = "";
                            string shoe_name = "";
                            string po_date = "";

                            Dictionary<string, object> data = new Dictionary<string, object>();
                            string putin_date = string.Empty;
                            #region old
                            //键值对传值
                            //data.Add("po", po);//po
                            //data.Add("art", art);//art_no
                            //string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                            //                            Program.Client.APIURL,
                            //                            "SJ_AQLAPI",//类库名
                            //                            "SJ_AQLAPI.AQL_Checkthedata1",//类名
                            //                            "Get_ArtInfo",//方法名
                            //                            Program.Client.UserToken,//token
                            //                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            //if (ret.IsSuccess)
                            //{
                            //    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                            //    art_name = dic["art_name"].ToString();
                            //    shoe_name = dic["shoe_name"].ToString();
                            //    po_date = dic["po_date"].ToString();
                            //}
                            //else
                            //    throw new Exception(ret.ErrMsg);
                            //using (APP_Compliance_Download_Print a = new APP_Compliance_Download_Print(GetAPP_Compliance_Maintenance(), GetAPP_Compliance_Download_DT(art, po_date), art, art_name, shoe_name))
                            //{
                            //    a.ShowDialog();
                            //}

                            #endregion
                            data.Add("art", art);//art_no
                            data.Add("po", textBox1.Text.Split('&')[0]);//PO
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                        Program.Client.APIURL,
                                                        "SJ_AQLAPI",//类库名
                                                        "SJ_AQLAPI.AQL_Checkthedata1",//类名
                                                        "Get_ArtFileInfo",//方法名
                                                        Program.Client.UserToken,//token
                                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                            //视图数据显示

                            if (ret.IsSuccess)
                            {
                                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                                string file_name = dt.Rows[0]["FILE_NAME"].ToString();
                                string file_url = Program.Client.PicUrl+ dt.Rows[0]["file_url"].ToString();
                                ShowFileHelper.ShowFile(file_url, file_name);
                            }
                            else
                            {
                                throw new Exception(ret.ErrMsg);
                            }
                            break;
                        case "Columnb2":
                            if (!string.IsNullOrWhiteSpace(dataGridView2.CurrentRow.Cells["Columnb2a"].Value.ToString()))
                            {
                                string art1 = textBox5.Text;
                                Dictionary<string, object> p = new Dictionary<string, object>();
                                p.Add("art", art1);//art_no
                                p.Add("TEST_TYPE", "0");//成品鞋类型
                                string retdata1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                            Program.Client.APIURL,
                                                            "SJ_AQLAPI",//类库名
                                                            "SJ_AQLAPI.AQL_Checkthedata1",//类名
                                                            "Get_ArtReportFileInfo",//方法名
                                                            Program.Client.UserToken,//token
                                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                                ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata1);
                                Dictionary<string, object> dic1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1.RetData);
                                //视图数据显示

                                if (ret1.IsSuccess)
                                {
                                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic1["Data"].ToString());
                                    string file_name = dt.Rows[0]["FILE_NAME"].ToString();
                                    string file_url = Program.Client.PicUrl + dt.Rows[0]["file_url"].ToString();
                                    ShowFileHelper.ShowFile(file_url, file_name);
                                }
                                else
                                {
                                    throw new Exception(ret1.ErrMsg);
                                }

                                //using (F_QCM_Ex_LookResult_New aa = new F_QCM_Ex_LookResult_New(dataGridView2.CurrentRow.Cells["Columnb2a"].Value.ToString(), Program.Client))
                                //{
                                //    //实验室结果(测检报告)
                                //    aa.ShowDialog();
                                //}
                            }
                            else
                            {
                                MessageBox.Show("No data found");//查无数据
                            }
                            break;
                        case "Columnb3":
                            if (!string.IsNullOrWhiteSpace(dataGridView2.CurrentRow.Cells["Columnb3a"].Value.ToString()))
                            {

                                //string art1 = textBox5.Text;
                                string PO = textBox1.Text;
                                Dictionary<string, object> p = new Dictionary<string, object>();
                                p.Add("PO", PO);//art_no
                                p.Add("TEST_TYPE", "4");//成品鞋类型
                                string retdata1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                            Program.Client.APIURL,
                                                            "SJ_AQLAPI",//类库名
                                                            "SJ_AQLAPI.AQL_Checkthedata1",//类名
                                                            "GetReport",//方法名
                                                            Program.Client.UserToken,//token
                                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                                ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata1);
                                Dictionary<string, object> dic1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1.RetData);
                                //视图数据显示

                                if (ret1.IsSuccess)
                                {
                                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic1["Data"].ToString());
                                    //string file_name = dt.Rows[0]["FILE_NAME"].ToString();
                                    //string file_url = Program.Client.PicUrl + dt.Rows[0]["file_url"].ToString();
                                    //ShowFileHelper.ShowFile(file_url, file_name);
                                    if(dt.Rows.Count > 0)
                                    {
                                        foreach (DataRow item in dt.Rows)
                                        {
                                            item["FILE_URL"] = Program.Client.PicUrl + item["FILE_URL"].ToString();
                                            item["net_file_url"] = Program.Client.PicUrl + item["net_file_url"].ToString();

                                        }
                                    }
                                    FrmFileListReadOnly frmFileList = new FrmFileListReadOnly(dt, Program.Client.UploadUrl, Program.Client.UserToken);
                                    //F_QCM_Ex_app_t_fileUpload_view add = new F_QCM_Ex_app_t_fileUpload_view(dt);
                                    frmFileList.Show();
                                }
                                else
                                {
                                    throw new Exception(ret1.ErrMsg);
                                }

                                //using (F_QCM_Ex_LookResult_New aa = new F_QCM_Ex_LookResult_New(dataGridView2.CurrentRow.Cells["Columnb3a"].Value.ToString(), Program.Client))
                                //{
                                //    //实验室结果(测检报告)
                                //    aa.ShowDialog();
                                //}
                            }
                            else
                            {
                                MessageBox.Show("No data found");//查无数据
                            }
                            break;

                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    switch (dataGridView3.Columns[e.ColumnIndex].Name)
                    {
                        case "Columnc1":
                            FrmShowFile add = new FrmShowFile(Program.Client.PicUrl+dataGridView3.CurrentRow.Cells["Columnc1v"].Value.ToString(), "");
                            add.Show();
                            break;
                        case "Columnc2":
                            FrmShowFile add2 = new FrmShowFile(Program.Client.PicUrl+dataGridView3.CurrentRow.Cells["Columnc2v"].Value.ToString(), "");
                            add2.Show();
                            break;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    switch (dataGridView4.Columns[e.ColumnIndex].Name)
                    {
                        case "Columnd1":
                            FrmShowFile add = new FrmShowFile(Program.Client.PicUrl+dataGridView4.CurrentRow.Cells["Columnd4"].Value.ToString(), "");
                            add.Show();
                            break;
                        case "Columnd2":
                            FrmShowFile add2 = new FrmShowFile(Program.Client.PicUrl+dataGridView4.CurrentRow.Cells["Columnd4"].Value.ToString(), "");
                            add2.Show();
                            break;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    switch (dataGridView5.Columns[e.ColumnIndex].Name)
                    {
                        case "Columne1":
                            FrmShowFile add = new FrmShowFile(Program.Client.PicUrl+dataGridView5.CurrentRow.Cells["Columne2"].Value.ToString(), "");
                            add.Show();
                            break;
                        case "Columne2":
                            FrmShowFile add2 = new FrmShowFile(Program.Client.PicUrl+dataGridView5.CurrentRow.Cells["Columne2"].Value.ToString(), "");
                            add2.Show();
                            break;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Dictionary<string, object> GetAPP_Compliance_Maintenance()
        {
            Dictionary<string, object> rdlcParam = new Dictionary<string, object>();
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_APP_Compliance",//类名
                                            "GetAPP_Compliance_Maintenance",//方法名
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
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    rdlcParam.Add("space_str_1", dt.Rows[0]["space_str_1"].ToString());
                    rdlcParam.Add("space_str_2", dt.Rows[0]["space_str_2"].ToString());
                    rdlcParam.Add("space_str_3", dt.Rows[0]["space_str_3"].ToString());
                    rdlcParam.Add("space_str_4", dt.Rows[0]["space_str_4"].ToString());
                    rdlcParam.Add("space_str_5", dt.Rows[0]["space_str_5"].ToString());
                    rdlcParam.Add("space_str_6", dt.Rows[0]["space_str_6"].ToString());
                    rdlcParam.Add("signature", Program.Client.PicUrl + dt.Rows[0]["FILE_URL"].ToString());
                    rdlcParam.Add("date", "");
                    rdlcParam.Add("prod_no", "");
                    rdlcParam.Add("prod_name", "");
                    rdlcParam.Add("shoe_name", "");
                    rdlcParam.Add("po", "");
                }
                else
                {
                    rdlcParam.Add("space_str_1", "");
                    rdlcParam.Add("space_str_2", "");
                    rdlcParam.Add("space_str_3", "");
                    rdlcParam.Add("space_str_4", "");
                    rdlcParam.Add("space_str_5", "");
                    rdlcParam.Add("space_str_6", "");
                    rdlcParam.Add("signature", "");
                    rdlcParam.Add("date", "");
                    rdlcParam.Add("prod_no", "");
                    rdlcParam.Add("prod_name", "");
                    rdlcParam.Add("shoe_name", "");
                    rdlcParam.Add("po", "");
                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return rdlcParam;
        }

        public DataTable GetAPP_Compliance_Download_DT(string art,string date)
        {
            DataTable Downloaddt = new DataTable();
            try
            {
                string fDate;
                try
                {
                    fDate = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    return new DataTable();
                }
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("DueDateS", fDate);
                data.Add("DueDateE", fDate);
                data.Add("prod_no", art);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_APP_Compliance",//类名
                                            "GetAPP_Compliance_Download",//方法名
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
                Downloaddt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return Downloaddt;
        }

        private void checkBoxa1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxa1.Checked)
            {
                autograph_code_a = curr_login_user_code;
                lbl_a.Text = curr_login_user_name;
            }
            else
            {
                autograph_code_a = "";
                lbl_a.Text = "";
            }
        }

        private void checkBoxb1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxb1.Checked)
            {
                autograph_code_b = curr_login_user_code;
                lbl_b.Text = curr_login_user_name;
            }
            else
            {
                autograph_code_b = "";
                lbl_b.Text = "";
            }
        }

        private void checkBoxc1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxc1.Checked)
            {
                autograph_code_c = curr_login_user_code;
                lbl_c.Text = curr_login_user_name;
            }
            else
            {
                autograph_code_c = "";
                lbl_c.Text = "";
            }
        }

        private void checkBoxd1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxd1.Checked)
            {
                autograph_code_d = curr_login_user_code;
                lbl_d.Text = curr_login_user_name;
            }
            else
            {
                autograph_code_d = "";
                lbl_d.Text = "";
            }
        }

        private void checkBoxe1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxe1.Checked)
            {
                autograph_code_e = curr_login_user_code;
                lbl_e.Text = curr_login_user_name;
            }
            else
            {
                autograph_code_e = "";
                lbl_e.Text = "";
            }
        }
    }
}
