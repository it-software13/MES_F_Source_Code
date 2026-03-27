using SJeMES_AQL.AQL_FrmBase;
using SJeMES_Framework.WebAPI;
using SJeMES_Report.AQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SJeMES_AQL
{
    public partial class F_AQL_PointBox : Form
    {
        F_AQL_CheckthedataMAX p_frm;
        Dictionary<string, object> dics = new Dictionary<string, object>();
        List<TestType> ttList = new List<TestType>();
        List<NewOldshoe> noList = new List<NewOldshoe>();
        string lot_num = string.Empty;
        public bool G_CLOSE = false;//生成任务是否关闭
        //Added for PO Change2 on 2025/02/13
        string mergeMark = string.Empty;
        DataTable referenceTable = null;

        public F_AQL_PointBox(Dictionary<string, object> _dics, F_AQL_CheckthedataMAX _p_frm)
        {
            InitializeComponent();
            p_frm = _p_frm;
            dics = _dics;
            InitializeControls();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            if (dics["pb_state"].ToString() == "1" || string.IsNullOrWhiteSpace(dics["pb_state"].ToString()))
            {
                button2.Visible = false;
                //button3.Text = "取消点箱完成";
            }
        }

        public void DisabledEdit()
        {
            try
            {
                if (dics["effective_status"].ToString() == "失效" || dics["pb_state"].ToString() == "1")
                {
                    //btn_insp_type.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button2.Enabled = false;
                    textBox6.Enabled = false;
                    dateTimePicker1.Enabled = false;
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    dataGridView1.ReadOnly = true;
                    dataGridView2.ReadOnly = true;
                }


                #region 检验结果
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("task_no", dics["task_no"]);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_PointBox",//类名
                                            "GetPointBox_AQLEDITSTATE",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                var AQL_EDIT_STATE = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(ret.RetData);

                if (AQL_EDIT_STATE == "1")
                {
                    label15.Visible = true;
                }
                else
                {
                    label15.Visible = false;
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //if (p_frm.AQL_EDIT_STATE[0] == "1")
            //{
            //    this.groupBox4.Visible = true;
            //}
            //else
            //{
            //    this.groupBox4.Visible = false;
            //}
            //label15.BringToFront();
        }

        //检验类型
        public class TestType
        {
            public string code { get; set; }
            public string value { get; set; }
        }

        //新旧鞋型
        public class NewOldshoe
        {
            public string code { get; set; }
            public string value { get; set; }
        }

        DataTable LevelDt = new DataTable();

        //初始化控件
        public void InitializeControls()
        {
            label32.Text = "";
            label33.Text = "";
            label34.Text = "";
            label35.Text = "";
            label36.Text = "";
            label19.Text = "";
            label37.Text = "";
        }

        /// <summary>
        /// 查询-点箱-头
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetPointBox_title()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("task_no", dics["task_no"]);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_PointBox",//类名
                                            "GetPointBox_title",//方法名
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

                if (dt.Rows.Count > 0)
                {
                    textBox5.Text = dt.Rows[0]["MER_PO"].ToString();
                    label35.Text = dt.Rows[0]["PROD_NO"].ToString();
                    label34.Text = dt.Rows[0]["SE_QTY"].ToString();
                    label32.Text = dt.Rows[0]["shoe_name"].ToString();
                    textBox6.Text = dt.Rows[0]["lot_num"].ToString();
                    lot_num = dt.Rows[0]["lot_num"].ToString();
                    comboBox4.SelectedValue = dt.Rows[0]["inspection_type"].ToString();
                    btn_insp_type.Text = ttList.First(x => x.code == comboBox4.SelectedValue.ToString()).value;
                    comboBox3.SelectedValue = dt.Rows[0]["shoe_type"].ToString();
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["inspection_date"].ToString()))
                    {
                        dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["inspection_date"].ToString());
                    }
                    else
                    {
                        dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    }
                    string is_inspection = dt.Rows[0]["is_inspection"].ToString();
                    if (is_inspection == "1")
                        dateTimePicker1.Enabled = false;
                    else
                        dateTimePicker1.Enabled = true;
                }
                label37.Text = dics["guojia"].ToString();
                label36.Text = dics["rule_no"].ToString();
                label33.Text = dics["CHECKER"].ToString();
                label19.Text = dics["from_line"].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void F_AQL_PointBox_Load(object sender, EventArgs e)
        {
            //this.splitContainer1.Panel1.Controls.Clear();
            //F_AQL_Inspection_GeneralInformation uc = new F_AQL_Inspection_GeneralInformation("点箱",dics["task_no"].ToString());
            ////uc.TopLevel = false;

            ////使用DockStyle进行填充
            //uc.Dock = System.Windows.Forms.DockStyle.Fill;
            ////将需要填充窗体的容器设置为窗体的父容器
            //// uc.Parent = this.splitContainer1.Panel1;
            ////使用内置函数ADD()进行窗体的添加
            //this.splitContainer1.Panel1.Controls.Add(uc);

            //this.FormBorderStyle = FormBorderStyle.None;
            ////this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            ////this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            ////this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            DisabledEdit();

            #region 检验类型
            TestType t1 = new TestType();
            t1.code = "0";
            t1.value = "Finally";//最终
            ttList.Add(t1);
            TestType t2 = new TestType();
            t2.code = "1";
            t2.value = "Rummage";//翻箱
            ttList.Add(t2);
            TestType t3 = new TestType();
            t3.code = "2";
            t3.value = "Again";//再次
            ttList.Add(t3);
            TestType t4 = new TestType();
            t4.code = "3";
            t4.value = "Rummage_Again";//再次翻箱
            ttList.Add(t4);
            comboBox4.DataSource = ttList;
            comboBox4.DisplayMember = "value";
            comboBox4.ValueMember = "code";
            #endregion

            #region 新旧鞋型
            NewOldshoe n2 = new NewOldshoe();
            n2.code = "0";
            n2.value = "Old_Shoes";//旧鞋型
            noList.Add(n2);
            NewOldshoe n1 = new NewOldshoe();
            n1.code = "1";
            n1.value = "New_Shoe_Type";//新鞋型
            noList.Add(n1);
            comboBox3.DataSource = noList;
            comboBox3.DisplayMember = "value";
            comboBox3.ValueMember = "code";
            #endregion

            CheckIsMergeOrder();

            GetPointBox_title();

            GetAQLEntry_RawLevel();

            GetAQLEntry_Sorting();

            GetPointBox();

            


        }

        /// <summary>
        /// 查询-点箱-样本级别/AQL级别
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetAQLEntry_RawLevel()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("task_no", dics["task_no"]);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.F_AQL_Entry",//类名
                                            "GetAQLEntry_RawLevel",//方法名
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
                var dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());
                var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
                var dt3 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data3"].ToString());

                comboBox1.DataSource = dt1;
                comboBox1.DisplayMember = "value";
                comboBox1.ValueMember = "code";

                comboBox2.DataSource = dt2;
                comboBox2.DisplayMember = "value";
                comboBox2.ValueMember = "code";

                if (dt3.Rows.Count > 0)
                {
                    comboBox1.SelectedValue = dt3.Rows[0]["sample_level"].ToString();
                    comboBox2.SelectedValue = dt3.Rows[0]["aql_level"].ToString();
                }
                else
                {
                    comboBox1.SelectedValue = "2";
                    comboBox2.SelectedValue = "AC13";
                }

                GetAQLPointBox_SamplingRate();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 查询-点箱-根据AQL级别获取抽样比例
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetAQLPointBox_SamplingRate()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("ac", comboBox2.SelectedValue.ToString());
                p.Add("num", dics["num"]);
                p.Add("LEVEL_TYPE", comboBox1.SelectedValue.ToString());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_PointBox",//类名
                                            "GetAQLPointBox_SamplingRate",//方法名
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
                var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
                var dt1213 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1213"].ToString());

                if (dt1213.Rows.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(dt1213.Rows[0]["VALS"].ToString()))
                    {
                        int ac12 = Convert.ToInt32(dt1213.Rows[0]["AC12"].ToString());//1.5
                        int ac13 = Convert.ToInt32(dt1213.Rows[0]["AC13"].ToString());//2.5
                        label22.Text = ac13.ToString();
                        label23.Text = (ac13 + 1).ToString();
                        label25.Text = ac12.ToString();
                        label26.Text = (ac12 + 1).ToString();
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["VALS"].ToString()))
                    {
                        decimal VALS = Convert.ToDecimal(dt.Rows[0]["VALS"].ToString());//样本数量
                        label8.Text = VALS.ToString();
                        decimal num = Convert.ToDecimal(dics["num"]);//任务数量
                        label6.Text = Math.Round((VALS / num) * 100, 2).ToString() + "%";
                        int ac = Convert.ToInt32(dt.Rows[0]["ac"].ToString());
                        label12.Text = ac.ToString();
                        label13.Text = (ac + 1).ToString();
                    }
                    GetAQLEntry_Sorting();
                }

                LevelDt = dt2;
                CalculateEvenNumbers();
                //if (dataGridView1.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //    {
                //        decimal ddl = Convert.ToDecimal(dataGridView1.Rows[i].Cells["订单量"].Value.ToString());
                //        DataRow[] dr = LevelDt.Select($@"START_QTY<={ddl} and END_QTY>={ddl}");
                //        if (dr.Length > 0)
                //            dataGridView1.Rows[i].Cells["双数"].Value = dr[0]["VALS"].ToString();
                //        else
                //            dataGridView1.Rows[i].Cells["双数"].Value = "0";
                //    }
                //}
                //if (dataGridView2.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                //    {
                //        decimal ddl = Convert.ToDecimal(dataGridView2.Rows[i].Cells["订单量2"].Value.ToString());
                //        DataRow[] dr = LevelDt.Select($@"START_QTY<={ddl} and END_QTY>={ddl}");
                //        if (dr.Length > 0)
                //            dataGridView2.Rows[i].Cells["双数2"].Value = dr[0]["VALS"].ToString();
                //        else
                //            dataGridView2.Rows[i].Cells["双数2"].Value = "0";
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region Commented on 2025/02/13 for PO Change2
        /// <summary>
        /// 计算双数
        /// </summary>
        //public void CalculateEvenNumbers()
        //{
        //    decimal sampleSize = 0;//样本量
        //    bool sampleSize_bool = decimal.TryParse(label8.Text, out sampleSize);
        //    decimal actualEvenNumber = 0;//实际双数
        //    bool actualEvenNumber_bool = decimal.TryParse(textBox6.Text, out actualEvenNumber);

        //    if (sampleSize_bool && actualEvenNumber_bool)
        //    {
        //        Dictionary<string, decimal> evenNumbersDic = new Dictionary<string, decimal>();
        //        if (dataGridView1 != null && dataGridView1.Rows.Count > 0)
        //        {
        //            int dataGridView1_index = 0;
        //            foreach (DataGridViewRow item in dataGridView1.Rows)
        //            {
        //                decimal curr_evenNumber = (Convert.ToDecimal(item.Cells["订单量"].Value.ToString()) / actualEvenNumber) * sampleSize;
        //                evenNumbersDic.Add($@"dataGridView1-{dataGridView1_index}", curr_evenNumber);
        //                dataGridView1_index++;
        //                //当前行的双数 
        //            }
        //        }
        //        if (dataGridView2 != null && dataGridView2.Rows.Count > 0)
        //        {
        //            int dataGridView2_index = 0;
        //            foreach (DataGridViewRow item in dataGridView2.Rows)
        //            {
        //                //当前行的双数
        //                decimal curr_evenNumber = (Convert.ToDecimal(item.Cells["订单量2"].Value.ToString()) / actualEvenNumber) * sampleSize;
        //                evenNumbersDic.Add($@"dataGridView2-{dataGridView2_index}", curr_evenNumber);
        //                dataGridView2_index++;
        //            }
        //        }

        //        //计算余数差值
        //        int addOne_count = Convert.ToInt32(sampleSize - evenNumbersDic.Sum(x => Math.Floor(x.Value)));

        //        evenNumbersDic = evenNumbersDic.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);

        //        string[] keys = evenNumbersDic.Keys.ToArray();
        //        for (int i = 0; i < keys.Length; i++)
        //        {
        //            if (i < addOne_count)
        //            {
        //                evenNumbersDic[keys[i]] = Math.Floor(evenNumbersDic[keys[i]]) + 1;
        //            }
        //            else
        //            {
        //                evenNumbersDic[keys[i]] = Math.Floor(evenNumbersDic[keys[i]]);
        //            }
        //        }

        //        foreach (var item in evenNumbersDic)
        //        {
        //            string[] key_info = item.Key.Split('-');
        //            if (key_info[0] == "dataGridView1")
        //            {
        //                dataGridView1.Rows[Convert.ToInt32(key_info[1])].Cells["双数"].Value = item.Value;
        //            }
        //            else if (key_info[0] == "dataGridView2")
        //            {
        //                dataGridView2.Rows[Convert.ToInt32(key_info[1])].Cells["双数2"].Value = item.Value;
        //            }
        //        }

        //    }
        //}

        #endregion
        /// <summary>
        /// 计算双数
        /// </summary>
        public void CalculateEvenNumbers()
        {

            decimal sampleSize = 0;//Sample size
            bool sampleSize_bool = decimal.TryParse(label8.Text, out sampleSize);
            decimal actualEvenNumber = 0;//Actual double number
            bool actualEvenNumber_bool = decimal.TryParse(textBox6.Text, out actualEvenNumber);

            if (sampleSize_bool && actualEvenNumber_bool)
            {
                if (!string.IsNullOrEmpty(mergeMark) && mergeMark.Equals("Y"))//If it is a combined order, execute the logic of combining orders
                {
                    Dictionary<string, decimal> evenNumbersDic = new Dictionary<string, decimal>();
                    if (dataGridView1 != null && dataGridView1.Rows.Count > 0)
                    {

                        // Assuming dataGridView1_index has been declared externally and initialized to 0
                        int dataGridView1_index = 0;

                        foreach (DataGridViewRow item in dataGridView1.Rows)
                        {
                            decimal curr_evenNumber = 0;

                            // Get the code number of the current row (handle null values)
                            string size = item.Cells["码数"].Value?.ToString() ?? string.Empty;

                            // Query all rows with the same code number from referenceTable (note that the field name is changed to size_no)
                            // Use single quotes to escape and specify the field name explicitly
                            string filterCondition = $"[cr_size] = '{size.Replace("'", "''")}'";
                            DataRow[] refRows = referenceTable.Select(filterCondition);

                            // Determine whether it is a combined size (number of rows ≥ 2)
                            if (refRows.Length >= 2)
                            {
                                foreach (DataRow row in refRows)
                                {
                                    // Safely obtain the value of se_qty
                                    object qtyObj = row["se_qty"];
                                    if (qtyObj != null && qtyObj != DBNull.Value && actualEvenNumber != 0)
                                    {
                                        decimal poLineQty = Convert.ToDecimal(qtyObj);
                                        curr_evenNumber += (poLineQty / actualEvenNumber) * sampleSize;
                                    }
                                }
                            }
                            else
                            {
                                // In the non-merge case, directly calculate the current row
                                object orderQtyObj = item.Cells["订单量"].Value;
                                if (orderQtyObj != null && orderQtyObj != DBNull.Value && actualEvenNumber != 0)
                                {
                                    decimal orderQty = Convert.ToDecimal(orderQtyObj);
                                    curr_evenNumber = (orderQty / actualEvenNumber) * sampleSize;
                                }
                            }

                            // Store the calculation results in a dictionary
                            evenNumbersDic.Add($@"dataGridView1-{dataGridView1_index}", curr_evenNumber);
                            dataGridView1_index++;
                        }
                    }


                    if (dataGridView2 != null && dataGridView2.Rows.Count > 0)
                    {
                        int dataGridView2_index = 0;
                        foreach (DataGridViewRow item in dataGridView2.Rows)
                        {
                            decimal curr_evenNumber = 0;

                            // Get the code number of the current row (assuming the column name is "Code Number 2")
                            string size = item.Cells["码数2"].Value?.ToString() ?? string.Empty;

                            // Query rows with the same size_no from referenceTable
                            DataRow[] refRows = referenceTable.Select($"[cr_size] = '{size.Replace("'", "''")}'");

                            // Merge logic (accumulate when the number of rows is ≥ 2)
                            if (refRows.Length >= 2)
                            {
                                foreach (DataRow row in refRows)
                                {
                                    object qtyObj = row["se_qty"];
                                    if (qtyObj != null && qtyObj != DBNull.Value && actualEvenNumber != 0)
                                    {
                                        decimal poLineQty = Convert.ToDecimal(qtyObj);
                                        curr_evenNumber += (poLineQty / actualEvenNumber) * sampleSize;
                                    }
                                }
                            }
                            else
                            {
                                //In the non-consolidated case, use the "Order Quantity 2" column to calculate
                                object orderQtyObj = item.Cells["订单量2"].Value;
                                if (orderQtyObj != null && orderQtyObj != DBNull.Value && actualEvenNumber != 0)
                                {
                                    decimal orderQty = Convert.ToDecimal(orderQtyObj);
                                    curr_evenNumber = (orderQty / actualEvenNumber) * sampleSize;
                                }
                            }

                            // Store in dictionary (key name distinguishes dataGridView2)
                            evenNumbersDic.Add($@"dataGridView2-{dataGridView2_index}", curr_evenNumber);
                            dataGridView2_index++;
                        }
                    }


                    //Calculate the remainder difference
                    int addOne_count = Convert.ToInt32(sampleSize - evenNumbersDic.Sum(x => Math.Floor(x.Value)));

                    evenNumbersDic = evenNumbersDic.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);

                    string[] keys = evenNumbersDic.Keys.ToArray();
                    for (int i = 0; i < keys.Length; i++)
                    {
                        if (i < addOne_count)
                        {
                            evenNumbersDic[keys[i]] = Math.Floor(evenNumbersDic[keys[i]]) + 1;
                        }
                        else
                        {
                            evenNumbersDic[keys[i]] = Math.Floor(evenNumbersDic[keys[i]]);
                        }
                    }

                    foreach (var item in evenNumbersDic)
                    {
                        string[] key_info = item.Key.Split('-');
                        if (key_info[0] == "dataGridView1")
                        {
                            dataGridView1.Rows[Convert.ToInt32(key_info[1])].Cells["双数"].Value = item.Value;
                        }
                        else if (key_info[0] == "dataGridView2")
                        {
                            dataGridView2.Rows[Convert.ToInt32(key_info[1])].Cells["双数2"].Value = item.Value;
                        }
                    }


                }
                else//Non-consolidated orders execute old logic
                {
                    Dictionary<string, decimal> evenNumbersDic = new Dictionary<string, decimal>();
                    if (dataGridView1 != null && dataGridView1.Rows.Count > 0)
                    {
                        int dataGridView1_index = 0;
                        foreach (DataGridViewRow item in dataGridView1.Rows)
                        {
                            //The double number of the current row
                            decimal curr_evenNumber = 0;
                            if (actualEvenNumber != 0)
                            {
                                curr_evenNumber = (Convert.ToDecimal(item.Cells["订单量"].Value.ToString()) / actualEvenNumber) * sampleSize;
                            }
                            evenNumbersDic.Add($@"dataGridView1-{dataGridView1_index}", curr_evenNumber);
                            dataGridView1_index++;
                        }
                    }
                    if (dataGridView2 != null && dataGridView2.Rows.Count > 0)
                    {
                        int dataGridView2_index = 0;
                        foreach (DataGridViewRow item in dataGridView2.Rows)
                        {
                            //The double number of the current row
                            decimal curr_evenNumber = 0;
                            if (actualEvenNumber != 0)
                            {
                                curr_evenNumber = (Convert.ToDecimal(item.Cells["订单量2"].Value.ToString()) / actualEvenNumber) * sampleSize;
                            }
                            evenNumbersDic.Add($@"dataGridView2-{dataGridView2_index}", curr_evenNumber);
                            dataGridView2_index++;
                        }
                    }

                    //Calculate the remainder difference
                    int addOne_count = Convert.ToInt32(sampleSize - evenNumbersDic.Sum(x => Math.Floor(x.Value)));

                    evenNumbersDic = evenNumbersDic.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);

                    string[] keys = evenNumbersDic.Keys.ToArray();
                    for (int i = 0; i < keys.Length; i++)
                    {
                        if (i < addOne_count)
                        {
                            evenNumbersDic[keys[i]] = Math.Floor(evenNumbersDic[keys[i]]) + 1;
                        }
                        else
                        {
                            evenNumbersDic[keys[i]] = Math.Floor(evenNumbersDic[keys[i]]);
                        }
                    }

                    foreach (var item in evenNumbersDic)
                    {
                        string[] key_info = item.Key.Split('-');
                        if (key_info[0] == "dataGridView1")
                        {
                            dataGridView1.Rows[Convert.ToInt32(key_info[1])].Cells["双数"].Value = item.Value;
                        }
                        else if (key_info[0] == "dataGridView2")
                        {
                            dataGridView2.Rows[Convert.ToInt32(key_info[1])].Cells["双数2"].Value = item.Value;
                        }
                    }
                }


            }
        }

        /// <summary>
        /// 查询-点箱-合计不良
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetAQLEntry_Sorting()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("task_no", dics["task_no"]);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.F_AQL_Entry",//类名
                                            "GetAQLEntry_Sorting",//方法名
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

                int zy = 0;//主要
                int cy = 0;//次要
                int yz = 0;//严重
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        switch (dr["problem_level"])
                        {
                            case "0":
                                zy += Convert.ToInt32(dr["bad_qty"].ToString());
                                break;
                            case "1":
                                cy += Convert.ToInt32(dr["bad_qty"].ToString());
                                break;
                            case "2":
                                yz += Convert.ToInt32(dr["bad_qty"].ToString());
                                break;
                            default:
                                break;
                        }
                    }
                }
                label24.Text = cy.ToString();
                label27.Text = zy.ToString();
                label30.Text = yz.ToString();

                int hjbl = cy + zy + yz;//合计不良

                label14.Text = hjbl.ToString();

                if (hjbl > Convert.ToInt32(label12.Text))
                {
                    label15.Text = "Rejected";
                    label15.ForeColor = Color.Red;
                }
                else
                {
                    label15.Text = "Accepted";
                    label15.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetAQLPointBox_SamplingRate();
        }

        /// <summary>
        /// 查询-点箱
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetPointBox()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("task_no", dics["task_no"]);
                p.Add("po", dics["po"]);
                p.Add("task_type", dics["task_type"]);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_PointBox",//类名
                                            "GetPointBox",//方法名
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
                var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());

                if (dt2.Rows[0]["pb_state"].ToString() == "1" || string.IsNullOrWhiteSpace(dt2.Rows[0]["pb_state"].ToString()))
                {
                    button2.Visible = false;
                    //button3.Text = "取消点箱完成";
                }

                if (dt.Rows.Count > 0)
                {
                    int resCount = dt.Rows.Count;
                    int dgvRowCount = 18;//dgv最大行数
                    int dgvCount = (resCount + dgvRowCount - 1) / dgvRowCount;//计算dgv个数

                    int xiangshu = 0;//箱数\
                    List<string> xsList = new List<string>();
                    for (int i = 0; i < dgvCount; i++)
                    {

                        int min = Math.Min(dgvRowCount, dt.Rows.Count);
                        switch (i)
                        {
                            case 0:
                                dataGridView1.Rows.Clear();
                                //分页读取接口返回数据
                                for (int a = 0; a < min; a++)
                                {
                                    dataGridView1.Rows.Add();
                                    DataGridViewRow dgvr = dataGridView1.Rows[a];
                                    dgvr.Cells["id"].Value = dt.Rows[a]["id"].ToString();
                                    dgvr.Cells["箱号"].Value = dt.Rows[a]["case_no"].ToString();
                                    if (!string.IsNullOrEmpty(dt.Rows[a]["case_no"].ToString()))
                                    {
                                        List<string> caselist = dt.Rows[a]["case_no"].ToString().Split('/').ToList();
                                        foreach (var item in caselist)
                                        {
                                            if (!xsList.Contains(item)&&!string.IsNullOrEmpty(item))
                                            {
                                                xsList.Add(item);
                                                xiangshu++;
                                            }
                                        }
                                        
                                    }
                                    dgvr.Cells["码数"].Value = dt.Rows[a]["cr_size"].ToString();
                                    dgvr.Cells["双数"].Value = "0";
                                    dgvr.Cells["订单量"].Value = dt.Rows[a]["se_qty"].ToString();
                                    dgvr.Cells["码数数量"].Value = dt.Rows[a]["size_qty"].ToString();
                                }
                                break;
                            case 1:
                                //分页读取接口返回数据
                                dataGridView2.Rows.Clear();
                                int b = 0;
                                //List<string> xs1List = new List<string>();
                                for (int a = dgvRowCount; a < dt.Rows.Count; a++)
                                {
                                    dataGridView2.Rows.Add();
                                    DataGridViewRow dgvr = dataGridView2.Rows[b];
                                    dgvr.Cells["id2"].Value = dt.Rows[a]["id"].ToString();
                                    dgvr.Cells["箱号2"].Value = dt.Rows[a]["case_no"].ToString();
                                    if (!string.IsNullOrEmpty(dt.Rows[a]["case_no"].ToString()))
                                    {
                                        List<string> caselist = dt.Rows[a]["case_no"].ToString().Split('/').ToList();
                                        foreach (var item in caselist)
                                        {
                                            if (!xsList.Contains(item) && !string.IsNullOrEmpty(item))
                                            {
                                                xsList.Add(item);
                                                xiangshu++;
                                            }
                                        }
                                    }
                                    dgvr.Cells["码数2"].Value = dt.Rows[a]["cr_size"].ToString();
                                    dgvr.Cells["双数2"].Value = "0";
                                    dgvr.Cells["订单量2"].Value = dt.Rows[a]["se_qty"].ToString();
                                    dgvr.Cells["码数数量2"].Value = dt.Rows[a]["size_qty"].ToString();
                                    b++;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    label7.Text = xiangshu.ToString();
                }

                CalculateEvenNumbers();
                //if (dataGridView1.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //    {
                //        decimal ddl = Convert.ToDecimal(dataGridView1.Rows[i].Cells["订单量"].Value.ToString());
                //        DataRow[] dr = LevelDt.Select($@"START_QTY<={ddl} and END_QTY>={ddl}");
                //        if (dr.Length > 0)
                //            dataGridView1.Rows[i].Cells["双数"].Value = dr[0]["VALS"].ToString();
                //        else
                //            dataGridView1.Rows[i].Cells["双数"].Value = "0";
                //    }
                //}
                //if (dataGridView2.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                //    {
                //        decimal ddl = Convert.ToDecimal(dataGridView2.Rows[i].Cells["订单量2"].Value.ToString());
                //        DataRow[] dr = LevelDt.Select($@"START_QTY<={ddl} and END_QTY>={ddl}");
                //        if (dr.Length > 0)
                //            dataGridView2.Rows[i].Cells["双数2"].Value = dr[0]["VALS"].ToString();
                //        else
                //            dataGridView2.Rows[i].Cells["双数2"].Value = "0";
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 保存-点箱-样本/aql级别
        /// </summary>
        public void EditPointBox_level()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("task_no", dics["task_no"].ToString());
                data.Add("sample_level", comboBox1.SelectedValue.ToString());
                data.Add("aql_level", comboBox2.SelectedValue.ToString());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_PointBox", "EditPointBox_level", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    MessageBox.Show("Submitted Successfully!");
                    GetAQLEntry_RawLevel(); 

                    GetAQLEntry_Sorting();
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
            EditPointBox_level();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "箱号")
                {
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    textBox4.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["箱号"].Value is null ? "" : dataGridView1.CurrentRow.Cells["箱号"].Value.ToString();
                    string 箱号 = aa == "" ? "" : aa;
                    textBox1.Text = 箱号;

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox1.Visible = true;
                    textBox1.Focus();
                }
                else if (dics["task_type"].ToString() != "自动生成")
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "订单量") // 
                    {
                        textBox1.Visible = false;
                        textBox3.Visible = false;
                        textBox4.Visible = false;
                        string aa = dataGridView1.CurrentRow.Cells["订单量"].Value is null ? "" : dataGridView1.CurrentRow.Cells["订单量"].Value.ToString();
                        string 订单量 = aa == "" ? "" : aa;
                        textBox2.Text = 订单量; //订单量

                        Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                        textBox2.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        textBox2.Visible = true;
                        textBox2.Focus();
                    }
                    else
                    {
                        textBox1.Visible = false;
                        textBox2.Visible = false;
                        textBox3.Visible = false;
                        textBox4.Visible = false;
                    }
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView2.Columns[e.ColumnIndex].Name == "箱号2") // 
                {
                    textBox2.Visible = false;
                    textBox1.Visible = false;
                    textBox4.Visible = false;
                    string aa = dataGridView2.CurrentRow.Cells["箱号2"].Value is null ? "" : dataGridView2.CurrentRow.Cells["箱号2"].Value.ToString();
                    string 箱号2 = aa == "" ? "" : aa;
                    textBox3.Text = 箱号2; //箱号2

                    Rectangle R = dataGridView2.GetCellDisplayRectangle(dataGridView2.CurrentCell.ColumnIndex, dataGridView2.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox3.SetBounds(R.X + dataGridView2.Location.X, R.Y + dataGridView2.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox3.Visible = true;
                    textBox3.Focus();
                }
                else if (dics["task_type"].ToString() != "自动生成")
                {
                    if (dataGridView2.Columns[e.ColumnIndex].Name == "订单量2") // 
                    {
                        textBox1.Visible = false;
                        textBox3.Visible = false;
                        textBox2.Visible = false;
                        string aa = dataGridView2.CurrentRow.Cells["订单量2"].Value is null ? "" : dataGridView2.CurrentRow.Cells["订单量2"].Value.ToString();
                        string 订单量2 = aa == "" ? "" : aa;
                        textBox4.Text = 订单量2; //订单量2

                        Rectangle R = dataGridView2.GetCellDisplayRectangle(dataGridView2.CurrentCell.ColumnIndex, dataGridView2.CurrentCell.RowIndex, false); //获取单元格位置 
                        textBox4.SetBounds(R.X + dataGridView2.Location.X, R.Y + dataGridView2.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        textBox4.Visible = true;
                        textBox4.Focus();
                    }
                    else
                    {
                        textBox1.Visible = false;
                        textBox2.Visible = false;
                        textBox3.Visible = false;
                        textBox4.Visible = false;
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Defines the set of allowed characters
            string allowedChars = "0123456789/";
            // Get the content of the current text box
            string currentText = textBox1.Text;
            // Use regular expression to replace all non-allowed characters with an empty string
            string filteredText = Regex.Replace(currentText, "[^" + Regex.Escape(allowedChars) + "]", "");
            // If the current text is different from the filtered text, update the text box content
            if (currentText != filteredText)
            {
                textBox1.Text = filteredText;
                // Set the selection position to the end to prevent the user's cursor from returning to the beginning of the text after illegal characters are removed
                textBox1.Select(textBox1.Text.Length, 0);
            }
            dataGridView1.CurrentCell.Value = textBox1.Text.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text.ToString()))
            {
                MessageBox.Show("不能为空!");
                return;
            }

            dataGridView1.CurrentCell.Value = textBox2.Text.ToString();

            decimal num = Convert.ToDecimal(textBox6.Text);//分批数量
            decimal sum = 0;//订单量和
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToDecimal(dataGridView1.Rows[i].Cells["订单量"].Value.ToString());
            }
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                sum += Convert.ToDecimal(dataGridView2.Rows[i].Cells["订单量2"].Value.ToString());
            }
            if (sum > num)
            {
                MessageBox.Show($"订单量总数不能大于分批数量【{num}】!");
                textBox2.Text = "0";
                dataGridView1.CurrentCell.Value = "0";
                return;
            }

            decimal ddl = Convert.ToDecimal(textBox2.Text.ToString());//订单量
            decimal size_qty = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["码数数量"].Value.ToString());//码数数量
            if (ddl > size_qty)
            {
                MessageBox.Show($"订单量不能大于码数数量【{size_qty}】!");
                textBox2.Text = "0";
                dataGridView1.CurrentCell.Value = "0";
                return;
            }

            //DataRow[] dr = LevelDt.Select($@"START_QTY<={ddl} and END_QTY>={ddl}");
            //if (dr.Length > 0)
            //    dataGridView1.CurrentRow.Cells["双数"].Value = dr[0]["VALS"].ToString();
            //else
            //    dataGridView1.CurrentRow.Cells["双数"].Value = "0";
            CalculateEvenNumbers();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.CurrentCell.Value = textBox3.Text.ToString();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text.ToString()))
            {
                MessageBox.Show("不能为空!");
                return;
            }

            decimal num = Convert.ToDecimal(textBox6.Text);//分批数量
            decimal sum = 0;//订单量和
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToDecimal(dataGridView1.Rows[i].Cells["订单量"].Value.ToString());
            }
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                sum += Convert.ToDecimal(dataGridView2.Rows[i].Cells["订单量2"].Value.ToString());
            }
            if (sum > num)
            {
                MessageBox.Show($"订单量总数不能大于分批数量【{num}】!");
                textBox4.Text = "0";
                return;
            }

            decimal ddl = Convert.ToDecimal(textBox4.Text.ToString());//订单量
            decimal size_qty = Convert.ToDecimal(dataGridView2.CurrentRow.Cells["码数数量2"].Value.ToString());//码数数量
            if (ddl > size_qty)
            {
                MessageBox.Show($"订单量不能大于码数数量【{size_qty}】!");
                textBox4.Text = "0";
                return;
            }

            //DataRow[] dr = LevelDt.Select($@"START_QTY<={ddl} and END_QTY>={ddl}");
            //if (dr.Length > 0)
            //    dataGridView2.CurrentRow.Cells["双数2"].Value = dr[0]["VALS"].ToString();
            //else
            //    dataGridView2.CurrentRow.Cells["双数2"].Value = "0";
            CalculateEvenNumbers();

            dataGridView2.CurrentCell.Value = textBox4.Text.ToString();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                e.Handled = true;

            if (e.KeyChar == (char)Keys.Enter)
            {
                int iColumn = dataGridView1.CurrentCell.ColumnIndex;
                int iRow = dataGridView1.CurrentCell.RowIndex;
                //if (iColumn == 1)
                {
                    if (iRow + 1 < dataGridView1.Rows.Count)
                    {
                        dataGridView1.CurrentCell = dataGridView1[iColumn, iRow + 1];
                        if (dics["task_type"].ToString() != "自动生成")
                        {
                            textBox1.Visible = false;
                            textBox3.Visible = false;
                            textBox4.Visible = false;
                            string aa = dataGridView1.CurrentRow.Cells["订单量"].Value is null ? "" : dataGridView1.CurrentRow.Cells["订单量"].Value.ToString();
                            string 订单量 = aa == "" ? "" : aa;
                            textBox2.Text = 订单量; //订单量

                            Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                            textBox2.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                            textBox2.Visible = true;
                            textBox2.Focus();
                        }
                    }

                }
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                e.Handled = true;

            if (e.KeyChar == (char)Keys.Enter)
            {
                int iColumn = dataGridView2.CurrentCell.ColumnIndex;
                int iRow = dataGridView2.CurrentCell.RowIndex;
                //if (iColumn == 1)
                {
                    if (iRow + 1 < dataGridView2.Rows.Count)
                    {
                        dataGridView2.CurrentCell = dataGridView2[iColumn, iRow + 1];
                        if (dics["task_type"].ToString() != "自动生成")
                        {
                            textBox1.Visible = false;
                            textBox3.Visible = false;
                            textBox2.Visible = false;
                            string aa = dataGridView2.CurrentRow.Cells["订单量2"].Value is null ? "" : dataGridView2.CurrentRow.Cells["订单量2"].Value.ToString();
                            string 订单量2 = aa == "" ? "" : aa;
                            textBox4.Text = 订单量2; //订单量2

                            Rectangle R = dataGridView2.GetCellDisplayRectangle(dataGridView2.CurrentCell.ColumnIndex, dataGridView2.CurrentCell.RowIndex, false); //获取单元格位置 
                            textBox4.SetBounds(R.X + dataGridView2.Location.X, R.Y + dataGridView2.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                            textBox4.Visible = true;
                            textBox4.Focus();
                        }
                    }

                }
            }

        }

        /// <summary>
        /// 保存-点箱-点箱完成/取消点箱完成
        /// </summary>
        public void EditPointBox_Complete()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("task_no", dics["task_no"].ToString());
                data.Add("state", 1);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_PointBox", "EditPointBox_Complete", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var pb_state = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(dic["pb_state"].ToString());
                var CHECKER = dic["CHECKER"].ToString();

                if (ret.IsSuccess)
                {
                    //MessageBox.Show("保存成功!");
                    if (pb_state == "0")
                    {
                        button3.Text = "点箱完成";
                        button2.Visible = true;
                    }
                    else
                    {
                        //button3.Text = "取消点箱完成";
                        button2.Visible = false;
                    }
                    dics["pb_state"] = "1";
                    dics["CHECKER"] = CHECKER;
                    DisabledEdit();
                    GetPointBox_title();
                    GetPointBox();
                }
                else
                    throw new Exception(ret.ErrMsg);
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to submit?", "Submit", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dr == DialogResult.OK)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["箱号"].Value.ToString()))
                    {
                        if (Convert.ToDecimal(dataGridView1.Rows[i].Cells["双数"].Value.ToString()) > 0)
                        {
                            MessageBox.Show("Box number cannot be empty!");
                            return;
                        }
                    }
                }
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(dataGridView2.Rows[i].Cells["箱号2"].Value.ToString()))
                    {
                        if (Convert.ToDecimal(dataGridView2.Rows[i].Cells["双数2"].Value.ToString()) > 0)
                        {
                            MessageBox.Show("Box number cannot be empty!");
                            return;
                        }
                    }
                }
                if (SavePointBox())
                    EditPointBox_Complete();
            }
        }

        /// <summary>
        /// 保存-点箱
        /// </summary>
        public bool EditPointBox(bool commit_res)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("task_no", dics["task_no"].ToString());
                data.Add("inspection_type", comboBox4.SelectedValue.ToString());
                data.Add("inspection_date", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                data.Add("shoe_type", comboBox3.SelectedValue.ToString());
                data.Add("lot_num", textBox6.Text.Trim());
                data.Add("list_m_pb_l", GetDgvToTable(dataGridView1));
                data.Add("list_m_pb_r", GetDgvToTable(dataGridView2));
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_PointBox", "EditPointBox", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    MessageBox.Show("Saved Successfully!");
                    GetPointBox_title();
                    GetPointBox();
                    commit_res = true;
                    return commit_res;
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return commit_res;
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

        private void button2_Click(object sender, EventArgs e)
        {
            SavePointBox();
        }

        private bool SavePointBox()
        {
            bool commit_res = false;
            decimal ltnum = 0;//分批数量
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
            //    if (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["箱号"].Value.ToString()))
            //    {
            //        MessageBox.Show("箱号不能为空!");
            //        return;
            //    }
                ltnum += Convert.ToDecimal(dataGridView1.Rows[i].Cells["订单量"].Value);
            }
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                //if (string.IsNullOrEmpty(dataGridView2.Rows[i].Cells["箱号2"].Value.ToString()))
                //{
                //    MessageBox.Show("箱号不能为空!");
                //    return;
                //}
                ltnum += Convert.ToDecimal(dataGridView2.Rows[i].Cells["订单量2"].Value);
            }

            if (textBox6.Text != ltnum.ToString())
            {
                MessageBox.Show("The sum of batch orders is inconsistent with the actual double number!");//分批订单量总和与实际双数不一致
                return commit_res;
            }

            commit_res = EditPointBox(commit_res);
            return commit_res;
        }

        /// <summary>
        /// 打印点箱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            GetPointBox_Print();
        }

        /// <summary>
        /// 打印-查询-点箱
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetPointBox_Print()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("task_no", dics["task_no"]);
                p.Add("po", dics["po"]);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_PointBox",//类名
                                            "GetPointBox_Print",//方法名
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
                var PointBoxdt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                CalculateEvenNumbers_Print(PointBoxdt);

                Dictionary<string, object> rdlcParam = new Dictionary<string, object>();
                rdlcParam.Add("PointBoxdt", PointBoxdt);

                if (p_frm.SourcePage == 0)
                    rdlcParam.Add("xiangshu", label7.Text);
                rdlcParam.Add("art", dics["art"]);
                rdlcParam.Add("shoe_name", dics["shoe_name"]);
                rdlcParam.Add("num", dics["num"]);
                rdlcParam.Add("num_total", label34.Text);
                rdlcParam.Add("po", dics["po"]);
                rdlcParam.Add("level", comboBox1.Text.Replace("一般检验水平", "").Replace("特殊检验水平", ""));//一般检验水平
                rdlcParam.Add("guojia", dics["guojia"]);
                rdlcParam.Add("sample_proportion", label6.Text);
                rdlcParam.Add("VALS", label8.Text);

                rdlcParam.Add("act", label12.Text);
                rdlcParam.Add("ac1", label22.Text);
                rdlcParam.Add("ac2", label25.Text);
                rdlcParam.Add("ac3", label28.Text);

                rdlcParam.Add("ret", label13.Text);
                rdlcParam.Add("re1", label23.Text);
                rdlcParam.Add("re2", label26.Text);
                rdlcParam.Add("re3", label29.Text);
                rdlcParam.Add("boxtype", comboBox4.SelectedValue.ToString());
                var Language = Program.Client.Language;
                //string value = string.Empty;
                switch (Language.ToLower())
                {
                    case "cn":
                        Language = "UI_CN";
                        break;
                    case "yn":
                        Language = "UI_YN";
                        break;
                    case "en":
                        Language = "UI_EN";
                        break;
                    default:
                        break;
                }
                using (PointBoxPrint h = new PointBoxPrint(rdlcParam, Program.Client.APIURL, Program.Client.UserToken, Language, p_frm.SourcePage))
                {
                    h.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CalculateEvenNumbers_Print(DataTable pointBoxdt)
        {
            decimal sampleSize = 0;//样本量
            bool sampleSize_bool = decimal.TryParse(label8.Text, out sampleSize);
            decimal actualEvenNumber = 0;//实际双数
            bool actualEvenNumber_bool = decimal.TryParse(textBox6.Text, out actualEvenNumber);

            if (sampleSize_bool && actualEvenNumber_bool)
            {
                Dictionary<int, decimal> evenNumbersDic = new Dictionary<int, decimal>();
                if (pointBoxdt != null && pointBoxdt.Rows.Count > 0)
                {
                    int datatable_index = 0;
                    foreach (DataRow item in pointBoxdt.Rows)
                    {
                        //当前行的双数
                        decimal curr_evenNumber = (Convert.ToDecimal(item["SE_QTY"].ToString()) / actualEvenNumber) * sampleSize;
                        evenNumbersDic.Add(datatable_index, curr_evenNumber);
                        datatable_index++;
                    }
                }

                //计算余数差值
                int addOne_count = Convert.ToInt32(sampleSize - evenNumbersDic.Sum(x => Math.Floor(x.Value)));

                evenNumbersDic = evenNumbersDic.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);

                int[] keys = evenNumbersDic.Keys.ToArray();
                for (int i = 0; i < keys.Length; i++)
                {
                    if (i < addOne_count)
                    {
                        evenNumbersDic[keys[i]] = Math.Floor(evenNumbersDic[keys[i]]) + 1;
                    }
                    else
                    {
                        evenNumbersDic[keys[i]] = Math.Floor(evenNumbersDic[keys[i]]);
                    }
                }

                foreach (var item in evenNumbersDic)
                {
                    pointBoxdt.Rows[item.Key]["SE_QTY"] = item.Value;
                }

            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void comboBox4_Click(object sender, EventArgs e)
        {
        }

        private void btn_insp_type_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("original_task_no", dics["task_no"].ToString());
            dic.Add("po", dics["po"].ToString());
            dic.Add("comboBox4", comboBox4.SelectedIndex.ToString());
            using (F_GenerateVerificationTask frm = new F_GenerateVerificationTask(dic, this))
            {
                frm.ShowDialog();
            }
            if (this.G_CLOSE)
                p_frm.Close();
        }

        private void dataGridView1_KeyPressdddd(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 'r')
            //{
            //    int iColumn = dataGridView1.CurrentCell.ColumnIndex;
            //    int iRow = dataGridView1.CurrentCell.RowIndex;
            //    if (iColumn == 1)
            //    {
            //        if (iColumn + 1 < dataGridView1.Columns.Count)
            //        {
            //            dataGridView1.CurrentCell = dataGridView1[iColumn, iRow+1];
            //        }
            //    }
            //}
            if (e.KeyChar == 'r')
            {
                DataGridView dgv = sender as DataGridView;
                DataGridViewCell cell = dgv.CurrentCell;
                if (cell.IsInEditMode)
                {
                    //限制单元格只能输入test 
                    if (cell.EditedFormattedValue != null && cell.EditedFormattedValue.ToString() != "test")
                    {
                        MessageBox.Show("输入内容不合格");
                    }
                    else
                    {
                        dgv.CurrentCell = dgv[cell.ColumnIndex, cell.RowIndex + 1];
                    }
                }
            }

        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'r')
            {
                int iColumn = dataGridView1.CurrentCell.ColumnIndex;
                int iRow = dataGridView1.CurrentCell.RowIndex;
                //if (iColumn == 1)
                {
                    if (iRow + 1 < dataGridView1.Rows.Count)
                    {
                        dataGridView1.CurrentCell = dataGridView1[iColumn, iRow + 1];
                    }
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int iColumn = dataGridView1.CurrentCell.ColumnIndex;
            int iRow = dataGridView1.CurrentCell.RowIndex;
            //if (iColumn == 1)
            {
                if (iRow + 1 < dataGridView1.Rows.Count)
                {
                    dataGridView1.CurrentCell = dataGridView1[iColumn, iRow + 1];
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                int iColumn = dataGridView1.CurrentCell.ColumnIndex;
                int iRow = dataGridView1.CurrentCell.RowIndex;
                //if (iColumn == 1)
                {
                    if (iRow + 1 < dataGridView1.Rows.Count)
                    {
                        dataGridView1.CurrentCell = dataGridView1[iColumn, iRow + 1];
                        textBox2.Visible = false;
                        textBox3.Visible = false;
                        textBox4.Visible = false;
                        string aa = dataGridView1.CurrentRow.Cells["箱号"].Value is null ? "" : dataGridView1.CurrentRow.Cells["箱号"].Value.ToString();
                        string 箱号 = aa == "" ? "" : aa;
                        textBox1.Text = 箱号; //箱号

                        Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                        textBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        textBox1.Visible = true;
                        textBox1.Focus();
                    }

                }
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                int iColumn = dataGridView2.CurrentCell.ColumnIndex;
                int iRow = dataGridView2.CurrentCell.RowIndex;
                //if (iColumn == 1)
                {
                    if (iRow + 1 < dataGridView2.Rows.Count)
                    {
                        dataGridView2.CurrentCell = dataGridView2[iColumn, iRow + 1];
                        textBox2.Visible = false;
                        textBox1.Visible = false;
                        textBox4.Visible = false;
                        string aa = dataGridView2.CurrentRow.Cells["箱号2"].Value is null ? "" : dataGridView2.CurrentRow.Cells["箱号2"].Value.ToString();
                        string 箱号2 = aa == "" ? "" : aa;
                        textBox3.Text = 箱号2; //箱号2

                        Rectangle R = dataGridView2.GetCellDisplayRectangle(dataGridView2.CurrentCell.ColumnIndex, dataGridView2.CurrentCell.RowIndex, false); //获取单元格位置 
                        textBox3.SetBounds(R.X + dataGridView2.Location.X, R.Y + dataGridView2.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        textBox3.Visible = true;
                        textBox3.Focus();
                    }
                }
            }
        }
        private void CheckIsMergeOrder()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            //Key-value pair transfer
            p.Add("mer_po", dics["po"]);
            p.Add("task_no", dics["task_no"]);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_AQLAPI",//类库名
                                        "SJ_AQLAPI.F_AQL_Entry",//类名
                                        "CheckIsMergeOrder",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (!ret.IsSuccess)
            {
                MessageBox.Show("Request failed：" + ret.ErrMsg);//throw new Exception(ret.ErrMsg);
            }

            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            mergeMark = dic["merge_mark"].ToString();
            referenceTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["pbReferenceTable"].ToString());
            //referenceTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["dt"].ToString());
         }
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
