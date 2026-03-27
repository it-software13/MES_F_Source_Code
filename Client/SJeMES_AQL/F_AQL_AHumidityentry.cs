using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_AQL.AQL_FrmBase;
using SJeMES_AQL.Common;
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
    
    public partial class F_AQL_AHumidityentry :Form
    {
        /// <summary>
        /// 从1开始
        /// </summary>
        private static int sum = 1;
        /// <summary>
        /// 执行条数
        /// </summary>
        private static int rowcoun = 18;
         private Dictionary<string,object> task = new Dictionary<string,object>();
        List<TestType> ttList = new List<TestType>();
        List<NewOldshoe> noList = new List<NewOldshoe>();
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
        public F_AQL_AHumidityentry(Dictionary<string,object> _task)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            task = _task;
            rowMergeView1.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 9.5f, FontStyle.Bold);
            rowMergeView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //表身样式,五号加粗
            rowMergeView1.RowHeadersDefaultCellStyle.Font = new Font("微软雅黑",9.5f, FontStyle.Bold);//左边序号样式
            rowMergeView1.RowsDefaultCellStyle.Font = new Font("微软雅黑", 9.5f);
            //禁止拖动列
            rowMergeView1.AllowUserToResizeColumns = false;
            rowMergeView2.AllowUserToResizeColumns = false;
            rowMergeView3.AllowUserToResizeColumns = false;
           
            rowMergeView1.RowTemplate.Height = 35;

            this.rowMergeView1.RowHeadersVisible = false;//隐藏第一列
            this.rowMergeView2.RowHeadersVisible = false;//隐藏第一列
            this.rowMergeView3.RowHeadersVisible = false;//隐藏第一列

            this.rowMergeView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;//隐藏表头hread

            DisabledEdit();
        }

        public void DisabledEdit()
        {
            if (task["effective_status"].ToString() == "Fail" || task["H_EDIT_STATE"].ToString() == "1")//失效
            {
                button1.Enabled = false;
                rowMergeView1.ReadOnly = true;
                rowMergeView2.ReadOnly = true;
                rowMergeView3.ReadOnly = true;
                btm_submit.Enabled = false;
            }
        }


        private void F_AQL_AHumidityentry_Load(object sender, EventArgs e)
        {
            //加载表头内容
            #region 检验类型
            //TestType t1 = new TestType();
            //t1.code = "0";
            //t1.value = "最终";
            //ttList.Add(t1);
            //TestType t2 = new TestType();
            //t2.code = "1";
            //t2.value = "翻箱";
            //ttList.Add(t2);
            //TestType t3 = new TestType();
            //t3.code = "2";
            //t3.value = "再次";
            //ttList.Add(t3);
            //TestType t4 = new TestType();
            //t4.code = "3";
            //t4.value = "再次翻箱";
            //ttList.Add(t4);
            //comboBox1.DataSource = ttList;
            //comboBox1.DisplayMember = "value";
            //comboBox1.ValueMember = "code";
            #endregion

            #region 新旧鞋型
            //NewOldshoe n1 = new NewOldshoe();
            //n1.code = "0";
            //n1.value = "新鞋型";
            //noList.Add(n1);
            //NewOldshoe n2 = new NewOldshoe();
            //n2.code = "1";
            //n2.value = "旧鞋型";
            //noList.Add(n2);
            //comboBox2.DataSource = noList;
            //comboBox2.DisplayMember = "value";
            //comboBox2.ValueMember = "code";
            #endregion

            //////

            F_AQL_Inspection_GeneralInformation uc = new F_AQL_Inspection_GeneralInformation("湿度录入", task);
            uc.Name = "shidu_uc";
            //uc.TopLevel = false;

            //使用DockStyle进行填充
            //uc.Dock = System.Windows.Forms.DockStyle.Fill;
            //将需要填充窗体的容器设置为窗体的父容器
            // uc.Parent = this.splitContainer1.Panel1;
            //使用内置函数ADD()进行窗体的添加
            this.Controls.Add(uc);

            this.FormBorderStyle = FormBorderStyle.None;
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            //this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            DataTable dt = data();
            rowMergeView1.Rows.Clear();
            if (dt.Rows.Count > 0 && rowMergeView1.Rows.Count<1)
            {
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    rowMergeView1.Rows.Add();
                    DataGridViewRow dgvr = rowMergeView1.Rows[i];
                    for (int j = sum; j < rowcoun; j++)
                    {
                        dgvr.Cells[$"Column{j}"].Value = dr[$"Column{j}"].ToString();
                    }
                    i++;
                }

            }
            //列标题⾼度
            this.rowMergeView1.ColumnHeadersHeight = 70;
            //设置不能调整列标题⾼度
            rowMergeView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.rowMergeView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //合并的列（columnName）
            for (int i = sum; i < rowcoun; i++)
            {
                this.rowMergeView1.MergeColumnNames.Add($"Column{i}");
            }
            //合并的列标题，第几列起，共几列
            this.rowMergeView1.AddSpanHeader(2, 7, "Finshed Shoes(Key material measure by parts)");//成品鞋（测量用到关键材料的部分）\n Finshed Shoes(Key material measure by parts)
            this.rowMergeView1.AddSpanHeader(9, 6, "Packing Material");//包装材料\n Packing Material)
            this.rowMergeView1.AddSpanHeader(1, 1, "Category");//类别\nType

            dt = data2();
            if (dt.Rows.Count > 0 && rowMergeView2.Rows.Count < 1)
            {
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    rowMergeView2.Rows.Add();
                    DataGridViewRow dgvr = rowMergeView2.Rows[i];
                    for (int j = sum; j < 10; j++)
                    {
                        dgvr.Cells["Columna1"].Value = dr["Columna1"].ToString();
                        dgvr.Cells["Columna2"].Value = dr["Columna2"].ToString();
                    }
                    i++;
                }
               
                this.rowMergeView2.MergeColumnNames.Add($"Columna1");
            }
            //加载底部编辑记录内容
            GetViewList();

            #region 定位控件位置
            rowMergeView1.MaximumSize = new Size(this.rowMergeView1.Width, 0);
            rowMergeView1.AutoSize = true;
            rowMergeView2.MaximumSize = new Size(this.rowMergeView2.Width, 0);
            rowMergeView2.AutoSize = true;
            rowMergeView3.MaximumSize = new Size(this.rowMergeView3.Width, 0);
            rowMergeView3.AutoSize = true;

            int x = 0;
            int y = 0;
            uc.Location = new Point(0, 0);

            x = 12;
            y = y + uc.Height + 5;
            btm_submit.Location = new Point(x, y);

            y = y + btm_submit.Height + 5;
            rowMergeView1.Location = new Point(x, y);

            y = y + rowMergeView1.Height + 5;
            rowMergeView2.Location = new Point(x, y);

            y = y + rowMergeView2.Height + 5;
            labela.Location = new Point(x, y);
            button1.Location = new Point(x + 200, y);

            y = y + button1.Height + 5;
            rowMergeView3.Location = new Point(x, y);


            int rowMergeView3_rowHeight = 0;
            foreach (DataGridViewRow view3_row in rowMergeView3.Rows)
            {
                rowMergeView3_rowHeight += view3_row.Height;
            }
            Rectangle R = rowMergeView3.GetCellDisplayRectangle(2, 0, false); //获取单元格位置 
            textBoxbuttom1.SetBounds(R.X + rowMergeView3.Location.X, R.Y + rowMergeView3.Location.Y, R.Width, rowMergeView3_rowHeight); //重新定位combobox.中间有坐标位置的转换 
            textBoxbuttom1.Multiline = true;
            textBoxbuttom1.Visible = true;

            R = rowMergeView3.GetCellDisplayRectangle(3, 0, false); //获取单元格位置 
            textBoxbuttom2.SetBounds(R.X + rowMergeView3.Location.X, R.Y + rowMergeView3.Location.Y, R.Width, rowMergeView3_rowHeight); //重新定位combobox.中间有坐标位置的转换 
            textBoxbuttom2.Multiline = true;
            textBoxbuttom2.Visible = true;

            R = rowMergeView3.GetCellDisplayRectangle(4, 0, false); //获取单元格位置 
            textBoxbuttom3.SetBounds(R.X + rowMergeView3.Location.X, R.Y + rowMergeView3.Location.Y, R.Width, rowMergeView3_rowHeight); //重新定位combobox.中间有坐标位置的转换 
            textBoxbuttom3.Multiline = true;
            textBoxbuttom3.Visible = true;
            #endregion
        }
        //private void textBox1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        try
        //        {

        //            //请求api的数据展示
        //            Dictionary<string, object> p = new Dictionary<string, object>();
        //            //键值对传值
        //            p.Add("po", textBox1.Text.Trim());
        //            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
        //                                        Program.Client.APIURL,
        //                                        "SJ_AQLAPI",//类库名
        //                                        "SJ_AQLAPI.AQL_CmaTask_Photo",//类名
        //                                        "GetInspection_GeneralInformationPo",//方法名
        //                                        Program.Client.UserToken,//token
        //                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));

        //            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
        //            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

        //            if (!ret.IsSuccess)
        //            {
        //                throw new Exception(ret.ErrMsg);
        //            }

        //            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
        //            //视图数据显示

        //            //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
        //            var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
        //            if (dt.Rows.Count > 0)
        //            {
        //                textBox2.Text = dt.Rows[0]["PROD_NO"].ToString();
        //                textBox5.Text = dt.Rows[0]["SE_QTY"].ToString();
        //                textBox3.Text = dt.Rows[0]["shoe_name"].ToString();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message);
        //        }
        //    }
        //}
        public void GetViewList()
        {
            if (!string.IsNullOrWhiteSpace(task["task_no"].ToString()))
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("task_no", task["task_no"].ToString());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_Checkthedata1",//类名
                                            "SduyMain",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
              
            
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        rowMergeView2.Rows[Convert.ToInt32(dr["bad_item_code"])].Cells[Convert.ToInt32(dr["bad_item_name"])].Value = dr["problem_level"].ToString();
                        i++;
                    }
                    dtrowviewdata = Auxiliary.GetDatagridviewDatable(rowMergeView2);
                }
               
               
            }
            ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_AQLAPI",//类库名
                                        "SJ_AQLAPI.AQL_Checkthedata1",//类名
                                        "SduyMainReamrk",//方法名
                                        Program.Client.UserToken, ""));
            if (!ret1.IsSuccess)
            {
                throw new Exception(ret1.ErrMsg);
            }
            Dictionary<string, object> dic1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1.RetData);
            DataTable dtret = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic1["data"].ToString());
            //加载底部（备注：Remark）
            rowMergeView3.Rows.Clear();
            rowMergeView3.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.rowMergeView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            string str1 = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            if (dtret.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in dtret.Rows)
                {
                    rowMergeView3.Rows.Add();
                    DataGridViewRow dgvr = rowMergeView3.Rows[i];
                    dgvr.Cells["Columnk1"].Value = dr["meterial"].ToString();
                    dgvr.Cells["Columnk2"].Value = dr["standard_value"].ToString();
                    dgvr.Cells["Columnk3"].Value ="";
                    dgvr.Cells["Columnk4"].Value = "";
                    dgvr.Cells["Columnk5"].Value ="";
                    if (i == 0)
                    {
                        str1 = dr["inspection"].ToString();
                        str2 = dr["measurement"].ToString();
                        str3 = dr["corrected_action"].ToString();
                    }
                    i++;
                }
                textBoxbuttom1.Text = str1;
                textBoxbuttom2.Text = str2;
                textBoxbuttom3.Text = str3;

            }
            rowMergeView3.ClearSelection();
            this.rowMergeView3.MergeColumnNames.Add($"Columnk3");
            this.rowMergeView3.MergeColumnNames.Add($"Columnk4");
            this.rowMergeView3.MergeColumnNames.Add($"Columnk5");
        }
        /// <summary>
        /// 表头数据
        /// </summary>
        /// <returns></returns>
        public static DataTable data()
        {
            DataTable dt = new DataTable();

            for (int i = sum; i < rowcoun; i++)
            {
                dt.Columns.Add($"Column{i}", typeof(string));
            }
            DataRow dr = dt.NewRow();
            for (int i = 0; i < 2; i++)
            {
                dr = dt.NewRow();
                dr["Column1"] = "No";//序号\nNo
                dr["Column2"] = "Name";//名称\nName
                dr["Column3"] = "Vamp";//鞋头\nVamp
                dr["Column4"] = "Feather\nQuarter";//鞋羽\nQuarter
                dr["Column5"] = "Heel";//后跟\nHeel
                dr["Column6"] = "Tongue";//鞋舌\nTongue
                dr["Column7"] = "Lining";//内里\nLining
                dr["Column8"] = "Insole";//鞋垫\nInsole
                dr["Column9"] = "Lace";
                dr["Column10"] = "lnsert paper";//塞纸\nlnsert paper
                dr["Column11"] = "Inner box";//内盒\nInner box
                dr["Column12"] = "Packing\npaper\n(front)";//包装纸（正）\nPacking\npaper\n(front)
                dr["Column13"] = "Packing\npaper\n(back)";//包装纸（反）\nPacking\npaper\n(back)
                dr["Column14"] = "Carton\n(in)";//外箱\n(内）\nCarton\n(in)
                dr["Column15"] = "Carton\n(out)";//外箱\n(外）\nCarton\n(out)
                dr["Column16"] = "Rejected\nitems";//不合格项\nRejected\nitems
                dr["Column17"] = "Corrected\nAction";//超标处理后\n记录\nCorrected\nAction
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 表头数据
        /// </summary>
        /// <returns></returns>
        public static DataTable data2()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add($"Columna1", typeof(string));
            dt.Columns.Add($"Columna2", typeof(string));
            DataRow dr = dt.NewRow();
            int z = 1;
            for (int i = 1; i < 21; i++)
            {

                dr = dt.NewRow();
                dr["Columna1"] = z;
                dr["Columna2"] = "Left_Foot";//L\n左脚
                if (i % 2 == 0)
                {
                    dr["Columna1"] = z;
                    z++;
                    dr["Columna2"] = "Right_Foot";//R\n右脚
                };
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private void rowMergeView2_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            //离开当前单元格就执行结束当前编辑
            //this.rowMergeView2.CurrentCell = null;
            //SendKeys.Send("{ENTER} ");
        }
        //用户离开编辑模式时发生。
        private void rowMergeView2_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > 1 && e.RowIndex > -1)
                {
                    if (!string.IsNullOrWhiteSpace(rowMergeView2.Rows[rowMergeView2.Rows.Count - 1].Cells[e.ColumnIndex].EditedFormattedValue.ToString()))
                    {
                        bool flag = true;
                        for (int j = 0; j < rowMergeView2.Rows.Count; j++)
                        {
                            if (!string.IsNullOrWhiteSpace(rowMergeView2.Rows[j].Cells[e.ColumnIndex].EditedFormattedValue.ToString()) && j != rowMergeView2.Rows.Count - 1)
                            {
                                flag = false;
                            }

                        }
                        if (flag)
                        {
                            for (int j = 0; j < rowMergeView2.Rows.Count; j++)
                            {
                                rowMergeView2.Rows[j].Cells[e.ColumnIndex].Value = rowMergeView2.Rows[rowMergeView2.Rows.Count - 1].Cells[e.ColumnIndex].EditedFormattedValue.ToString();
                            }
                          
                        }
                       
                    }
                    key = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private DataTable dtrowviewdata = new DataTable();
        private bool key = false;
        private void button1_Click(object sender, EventArgs e)
        {
            SaveCommit();
        }

        public bool SaveCommit()
        {
            bool save_result = false;

            try
            {
                if (rowMergeView2.Rows.Count > 0 && key == true)
                {
                    DataTable dt1 = Auxiliary.GetDatagridviewDatable(rowMergeView2);
                    IEnumerable<DataRow> query = dt1.AsEnumerable().Except(dtrowviewdata.AsEnumerable(), DataRowComparer.Default);
                    DataTable dt2 = query.CopyToDataTable();//求差集
                    List<int> index = new List<int>();
                    foreach (DataRow row in dt2.Rows)
                    {
                        //6=>左脚上>右脚下
                       // if (row[1].ToString().Contains("左脚"))
                        if (row[1].ToString().Contains("Left_Foot"))
                            index.Add(Convert.ToInt32(row[0].ToString()) * 2 - 1);
                        else
                            index.Add(Convert.ToInt32(row[0].ToString()) * 2);

                    }
                    List<Dictionary<string, object>> diclist = new List<Dictionary<string, object>>();
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    for (int i = 0; i < index.Count; i++)
                    {
                        for (int j = 2; j < rowMergeView2.ColumnCount; j++)
                        {
                            if (rowMergeView2.Rows[index[i] - 1].Cells[j].Value != null)
                            {
                                dic = new Dictionary<string, object>();
                                dic.Add("bad_item_code", index[i] - 1);//行坐标
                                dic.Add("bad_item_name", j);//列坐标
                                dic.Add("problem_level", rowMergeView2.Rows[index[i] - 1].Cells[j].Value);//内容值
                                diclist.Add(dic);
                            }

                        }
                    }
                    dic = new Dictionary<string, object>();
                    dic.Add("diclist", diclist);
                    dic.Add("task_no", task["task_no"].ToString());
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                 "SJ_AQLAPI", "SJ_AQLAPI.AQL_Checkthedata1", "SduyCommit", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dic));
                    var k = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                    if (Convert.ToBoolean(k["IsSuccess"].ToString()))
                    {
                        save_result = true;
                        MessageBox.Show("Saved successfully");
                    }
                    else
                    {
                        MessageBox.Show("Save_Failed, Reason," + k["ErrMsg"].ToString());
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return save_result;
        } 

        private void btm_submit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to submit!", "Submit", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dr == DialogResult.OK)
            {
                var rres = SaveCommit();
                if (!rres)
                    return;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task["task_no"].ToString());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_Checkthedata1", "EditHState", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (ret.IsSuccess)
                {
                    task["H_EDIT_STATE"] = "1";
                    DisabledEdit();
                }
                else
                {
                    throw new Exception(ret.ErrMsg);
                }
            }
        }
    }
}
