using Newtonsoft.Json;
using SJ_AQLAPI.DTO;
using SJeMES_Control_Library;
using SJeMES_Control_Library.Forms;
using SJeMES_Control_Library.VideoCapture;
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
using static SJeMES_AQL.Common.Enum;

namespace SJeMES_AQL.AQL_FrmBase
{
    public partial class F_AQL_Entry : Form
    {
        F_AQL_CheckthedataMAX p_frm;
        F_AQL_Inspection_GeneralInformation _uc;
        Dictionary<string, object> dics = new Dictionary<string, object>();
        public F_AQL_Entry(Dictionary<string, object> _dics, F_AQL_CheckthedataMAX _p_frm)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            p_frm = _p_frm;
            dics = _dics;
            DisabledEdit();
        }

        public void DisabledEdit()
        {
            if (dics["effective_status"].ToString() == "失效" || dics["AQL_EDIT_STATE"].ToString() == "1")
            {
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                button1.Enabled = false;
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                dataGridView4.ReadOnly = true;
            }
            else
            {
                //label15.Visible = false;
                dataGridView4.ReadOnly = false;
            }


            #region Here and above Commented label15 Visible and Not visible logic to show AQL result even beofore final submit by Ashok on 2026/01/28
            //if (p_frm.AQL_EDIT_STATE[0] == "1")
            //{
            //    label15.Visible = true;
            //}
            //else
            //{
            //    label15.Visible = false;
            //}
            //label15.Refresh();
            #endregion
        }

        private void F_AQL_Entry_Load(object sender, EventArgs e)
        {
            this.splitContainer1.Panel1.Controls.Clear();
            F_AQL_Inspection_GeneralInformation uc = new F_AQL_Inspection_GeneralInformation("AQL_Entry", dics);//AQL录入
            //uc.TopLevel = false;

            //使用DockStyle进行填充
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            //将需要填充窗体的容器设置为窗体的父容器
            // uc.Parent = this.splitContainer1.Panel1;
            //使用内置函数ADD()进行窗体的添加
            this.splitContainer1.Panel1.Controls.Add(uc);
            _uc = uc;

            this.FormBorderStyle = FormBorderStyle.None;
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            //this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            GetAQLEntry_RawLevel();

            GetAQLEntry_Classify(string.Empty);

            GetAQLEntry_Sorting();



            //SJeMES_Framework.Common.UIHelper.LoadDgv(dataGridView2);
            //SJeMES_Framework.Common.UIHelper.LoadDgv(dataGridView3);
            //SJeMES_Framework.Common.UIHelper.LoadDgv(dataGridView4);
        }

        /// <summary>
        /// 查询-AQL录入-样本级别/AQL级别
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

                GetAQLEntry_SamplingRate();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 查询-AQL录入-不良分类/不良项目
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetAQLEntry_Classify(string bad_classify_code)
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("bad_classify_code", bad_classify_code);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.F_AQL_Entry",//类名
                                            "GetAQLEntry_Classify",//方法名
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

                if (string.IsNullOrWhiteSpace(bad_classify_code))
                {
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView2.Rows.Clear();
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView2.Rows.Add();
                            DataGridViewRow dgvr = dataGridView2.Rows[i];
                            dgvr.Cells["不良分类代号"].Value = dr["bad_classify_code"].ToString();
                            dgvr.Cells["不良分类名称"].Value = dr["bad_classify_name"].ToString();
                            i++;
                        }
                    }
                }
                else
                {
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView3.Rows.Clear();
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView3.Rows.Add();
                            DataGridViewRow dgvr = dataGridView3.Rows[i];
                            dgvr.Cells["不良分类代号2"].Value = dr["bad_classify_code"].ToString();
                            dgvr.Cells["不良项目代号"].Value = dr["bad_item_code"].ToString();
                            dgvr.Cells["不良项目名称"].Value = dr["bad_item_name"].ToString();
                            dgvr.Cells["问题级别"].Value = dr["problem_level"].ToString();
                            if (dr["problem_level"].ToString() == "0")
                            {
                                dgvr.Cells["问题级别名称"].Value = "Major";//主要
                            }
                            else if (dr["problem_level"].ToString() == "1")
                            {
                                dgvr.Cells["问题级别名称"].Value = "Minor";//次要
                            }
                            else if (dr["problem_level"].ToString() == "2")
                            {
                                dgvr.Cells["问题级别名称"].Value = "Critical";//严重
                            }
                            else
                            {
                                dgvr.Cells["问题级别名称"].Value = "";
                            }
                            i++;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                string bad_classify_code = dataGridView2.Rows[e.RowIndex].Cells["不良分类代号"].Value.ToString();
                GetAQLEntry_Classify(bad_classify_code);
            }
        }
        #region Commented for new defect codes logic
        //private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        //{

        //    if (e.RowIndex >= 0 && e.ColumnIndex > -1)
        //    {
        //        string bad_item_code = dataGridView3.Rows[e.RowIndex].Cells["不良项目代号"].Value.ToString();
        //        bool isexist = false;//判断排序里是否存在该条数据
        //        for (int i = 0; i < dataGridView4.Rows.Count; i++)
        //        {
        //            if (dataGridView4.Rows[i].Cells["不良项目代号2"].Value.ToString() == bad_item_code)
        //            {
        //                isexist = true;
        //                dataGridView4.Rows[i].Cells["不良数量"].Value = Convert.ToInt32(dataGridView4.Rows[i].Cells["不良数量"].Value) + 1;
        //            }
        //        }
        //        if (!isexist)
        //        {
        //            int i = dataGridView4.Rows.Add();
        //            dataGridView4.Rows[i].Cells["不良项目代号2"].Value = bad_item_code;
        //            dataGridView4.Rows[i].Cells["不良项目名称2"].Value = dataGridView3.Rows[e.RowIndex].Cells["不良项目名称"].Value.ToString();
        //            dataGridView4.Rows[i].Cells["不良数量"].Value = "1";
        //            dataGridView4.Rows[i].Cells["问题级别2"].Value = dataGridView3.Rows[e.RowIndex].Cells["问题级别"].Value.ToString();
        //            dataGridView4.Rows[i].Cells["问题级别名称2"].Value = dataGridView3.Rows[e.RowIndex].Cells["问题级别名称"].Value.ToString();
        //            dataGridView4.Rows[i].Cells["不良分类代号3"].Value = dataGridView3.Rows[e.RowIndex].Cells["不良分类代号2"].Value.ToString();
        //            dataGridView4.Rows[i].Cells["imglist"].Value = "";
        //        }

        //    }
        //    #region Added by Ashok on 2026/01/28 to show defect count and Pass fail Status before Final save
        //    UpdateDefetctLabelCount();
        //    #endregion
        //}
        #endregion


        // new for defect code new  given by APH
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                string bad_item_code = dataGridView3.Rows[e.RowIndex].Cells["不良项目代号"].Value.ToString();
                string problem_level = dataGridView3.Rows[e.RowIndex].Cells["问题级别"].Value.ToString();

                bool isExist = false;

                for (int i = 0; i < dataGridView4.Rows.Count; i++)
                {
                    // 同时判断 不良项目代号 + 问题级别
                    //Simultaneously determine the problematic project code +issue level.
                    if (dataGridView4.Rows[i].Cells["不良项目代号2"].Value.ToString() == bad_item_code
                        && dataGridView4.Rows[i].Cells["问题级别2"].Value.ToString() == problem_level)
                    {
                        // 满足 2 条件 → cộng số lượng
                        // Satisfies condition 2 → cộng số lượng
                        dataGridView4.Rows[i].Cells["不良数量"].Value = Convert.ToInt32(dataGridView4.Rows[i].Cells["不良数量"].Value) + 1;

                        isExist = true;
                        break;
                    }
                }

                // 不 tồn tại → thêm dòng mới
                if (!isExist)
                {
                    int i = dataGridView4.Rows.Add();
                    dataGridView4.Rows[i].Cells["不良项目代号2"].Value = bad_item_code;
                    dataGridView4.Rows[i].Cells["不良项目名称2"].Value = dataGridView3.Rows[e.RowIndex].Cells["不良项目名称"].Value.ToString();
                    dataGridView4.Rows[i].Cells["不良数量"].Value = 1;
                    dataGridView4.Rows[i].Cells["问题级别2"].Value = problem_level;
                    dataGridView4.Rows[i].Cells["问题级别名称2"].Value = dataGridView3.Rows[e.RowIndex].Cells["问题级别名称"].Value.ToString();
                    dataGridView4.Rows[i].Cells["不良分类代号3"].Value = dataGridView3.Rows[e.RowIndex].Cells["不良分类代号2"].Value.ToString();
                    dataGridView4.Rows[i].Cells["imglist"].Value = "";
                }
            }
            #region Added by Ashok on 2026/01/28 to show defect count and Pass fail Status before Final save
            UpdateDefetctLabelCount();
            #endregion
        }


        private void UpdateDefetctLabelCount()
        {
            int criticalTotal = 0;
            int majorTotal = 0;
            int minorTotal = 0;
            int TotalDefects = 0;

            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (row.IsNewRow) continue;

                string level = Convert.ToString(row.Cells["问题级别名称2"].Value);
                int qty = 0;

                int.TryParse(Convert.ToString(row.Cells["不良数量"].Value), out qty);

                switch (level)
                {
                    case "Critical":
                        criticalTotal += qty;
                        break;

                    case "Major":
                        majorTotal += qty;
                        break;

                    case "Minor":
                        minorTotal += qty;
                        break;
                }
                TotalDefects += qty;

            }

            label24.Text = minorTotal.ToString();
            label27.Text = majorTotal.ToString();
            label30.Text = criticalTotal.ToString();
            label14.Text = TotalDefects.ToString();
            OnLabelUpdated();
        }

        private void OnLabelUpdated()
        {
            #region New Logic  to check each criteria wise rejected qty insted of three criterias combined
            if ((Convert.ToInt32(label24.Text) >= Convert.ToInt32(label23.Text)) || (Convert.ToInt32(label27.Text) >= Convert.ToInt32(label26.Text)) || (Convert.ToInt32(label30.Text) >= Convert.ToInt32(label29.Text)))
            {
                label15.Text = "Rejected";
                label15.ForeColor = Color.Red;
            }
            else
            {
                label15.Text = "Accepted";
                label15.ForeColor = Color.Green;
            }
            #endregion
        }


        /// <summary>
        /// 查询-AQL录入-不良排序-图片
        /// </summary>
        /// <returns></returns>
        public DataTable Getimage_guid(string image_guid)
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("image_guid", image_guid);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_AQLAPI",//类库名
                                        "SJ_AQLAPI.F_AQL_Entry",//类名
                                        "Getimage_guid",//方法名
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
                dt.Columns.Add("net_file_url", typeof(string));
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["file_url"].ToString()))
                    {
                        try
                        {
                            dr["net_file_url"] = Program.Client.PicUrl + dr["file_url"].ToString();
                        }
                        catch
                        {
                        }
                    }
                    i++;
                }
            }
            return dt;
        }

        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView4.Columns[e.ColumnIndex].Name == "上传照片" && !dataGridView4.ReadOnly)//上传照片
                {
                    //创建文件弹出选择窗口（包括文件名）对象
                    OpenFileDialog ofd = new OpenFileDialog();
                    //判断选择的路径
                    string path = string.Empty;
                    ofd.Title = "请选择文件夹";
                    ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
                    ofd.Multiselect = true;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        foreach (var item in ofd.FileNames)
                        {
                            SafeFileName = System.IO.Path.GetFileName(item);
                            filePath = item;
                            UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                            if (res.IsSuccess)
                            {
                                var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                                if (dataGridView4.Rows[e.RowIndex].Cells["imglist"].Value != null && !string.IsNullOrEmpty(dataGridView4.Rows[e.RowIndex].Cells["imglist"].Value.ToString()))
                                {
                                    dataGridView4.Rows[e.RowIndex].Cells["imglist"].Value = dataGridView4.Rows[e.RowIndex].Cells["imglist"].Value + "," + resultDIC["guid"].ToString();
                                }
                                else
                                {
                                    dataGridView4.Rows[e.RowIndex].Cells["imglist"].Value = resultDIC["guid"].ToString();
                                }
                                //string union_id = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                                //TQC_Task_Edit_Upload(union_id, resultDIC["guid"].ToString());
                                MessageBox.Show("上传成功");
                            }
                        }

                    }
                }


                if (dataGridView4.Columns[e.ColumnIndex].Name == "拍照" && !dataGridView4.ReadOnly)
                {
                    TakePh(e);
                }
                
                if (dataGridView4.Columns[e.ColumnIndex].Name == "查看")
                {
                    var currRowFileDt = Getimage_guid(dataGridView4.Rows[e.RowIndex].Cells["imglist"].Value.ToString());
                    FrmFileList add = new FrmFileList(currRowFileDt, Program.Client.APIURL, Program.Client.UserToken, "", false);
                    add.ShowDialog();
                    int i = 0;
                    string image_guids = string.Empty;
                    foreach (DataRow item in currRowFileDt.Rows)
                    {
                        image_guids += item["guid"];
                        if (i < currRowFileDt.Rows.Count - 1)
                        {
                            image_guids += ",";
                        }
                        i++;
                    }
                    dataGridView4.Rows[e.RowIndex].Cells["imglist"].Value = image_guids;

                    //SJeMES_Control_Library.Forms.FrmImgList fil = new SJeMES_Control_Library.Forms.FrmImgList(Getimage_guid(dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value.ToString()), null, "");
                    //fil.ShowDialog();
                }
                if (dataGridView4.Columns[e.ColumnIndex].Name == "数量减一" && !dataGridView4.ReadOnly)
                {
                    if (dataGridView4.Rows[e.RowIndex].Cells["不良数量"].Value.ToString() == "1")
                        dataGridView4.Rows.RemoveAt(e.RowIndex);
                    else
                    {
                        dataGridView4.Rows[e.RowIndex].Cells["不良数量"].Value = Convert.ToInt32(dataGridView4.Rows[e.RowIndex].Cells["不良数量"].Value) - 1;
                    }
                    #region Added by Ashok on 2026/01/28 to show defect count and Pass fail Status before Final save
                    UpdateDefetctLabelCount();
                    #endregion
                }
            }
        }

        /// <summary>
        /// 查询-AQL录入-根据AQL级别获取抽样比例
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetAQLEntry_SamplingRate()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("ac", comboBox2.SelectedValue.ToString());
                p.Add("num", dics["num"]);
                p.Add("LEVEL_TYPE", comboBox1.SelectedValue.ToString());

                p.Add("task_no", dics["task_no"]);
                p.Add("po", dics["po"]);
                p.Add("task_type", dics["task_type"]);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.F_AQL_Entry",//类名
                                            "GetAQLEntry_SamplingRate",//方法名
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
                var dt1213 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1213"].ToString());
                var Datadx = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Datadx"].ToString());
                int ac12=0;
                int ac13=0;
                if (dt1213.Rows.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(dt1213.Rows[0]["VALS"].ToString()))
                    {
                        ac12 = Convert.ToInt32(dt1213.Rows[0]["AC12"].ToString());//1.5
                        ac13 = Convert.ToInt32(dt1213.Rows[0]["AC13"].ToString());//2.5
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
                        //int ac = Convert.ToInt32(dt.Rows[0]["ac"].ToString());
                        int ac = ac13+ ac12;
                        label12.Text = ac.ToString();
                        label13.Text = (ac + 1).ToString();
                       // label28.Text = ac.ToString();
                        //label29.Text = (ac + 1).ToString();
                    }
                    GetAQLEntry_Sorting();
                }

                //点箱数
                if (Datadx.Rows.Count > 0)
                {
                    int resCount = Datadx.Rows.Count;
                    int dgvRowCount = 18;//dgv最大行数
                    int dgvCount = (resCount + dgvRowCount - 1) / dgvRowCount;//计算dgv个数
                    List<string> xsList = new List<string>();
                    int xiangshu = 0;//箱数
                    for (int i = 0; i < dgvCount; i++)
                    {
                        int min = Math.Min(dgvRowCount, Datadx.Rows.Count);
                        switch (i)
                        {
                            case 0:
                                //分页读取接口返回数据
                                for (int a = 0; a < min; a++)
                                {

                                    if (!string.IsNullOrEmpty(Datadx.Rows[a]["case_no"].ToString()))
                                    {
                                        List<string> caselist = Datadx.Rows[a]["case_no"].ToString().Split('/').ToList();
                                        foreach (var item in caselist)
                                        {
                                            if (!xsList.Contains(item) && !string.IsNullOrEmpty(item))
                                            {
                                                xsList.Add(item);
                                                xiangshu++;
                                            }
                                        }

                                    }

                                }
                                break;
                            case 1:
                                //分页读取接口返回数据
                                int b = 0;
                                for (int a = dgvRowCount; a < Datadx.Rows.Count; a++)
                                {
                                    if (!string.IsNullOrEmpty(Datadx.Rows[a]["case_no"].ToString()))
                                    {
                                        List<string> caselist = Datadx.Rows[a]["case_no"].ToString().Split('/').ToList();
                                        foreach (var item in caselist)
                                        {
                                            if (!xsList.Contains(item) && !string.IsNullOrEmpty(item))
                                            {
                                                xsList.Add(item);
                                                xiangshu++;
                                            }
                                        }

                                    }

                                    b++;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    label7.Text = xiangshu.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetAQLEntry_SamplingRate();
        }

        /// <summary>
        /// 查询-AQL录入-不良排序
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
                    dataGridView4.Rows.Clear();
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView4.Rows.Add();
                        DataGridViewRow dgvr = dataGridView4.Rows[i];
                        dgvr.Cells["不良分类代号3"].Value = dr["bad_classify_code"].ToString();
                        dgvr.Cells["不良项目代号2"].Value = dr["bad_item_code"].ToString();
                        dgvr.Cells["不良项目名称2"].Value = dr["bad_item_name"].ToString();
                        dgvr.Cells["问题级别2"].Value = dr["problem_level"].ToString();
                        dgvr.Cells["不良数量"].Value = dr["bad_qty"].ToString();
                        dgvr.Cells["imglist"].Value = dr["imglist"].ToString();
                        i++;

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


                        if (dr["problem_level"].ToString() == "0")
                        {
                            dgvr.Cells["问题级别名称2"].Value = "Major";
                        }
                        else if (dr["problem_level"].ToString() == "1")
                        {
                            dgvr.Cells["问题级别名称2"].Value = "Minor";
                        }
                        else if (dr["problem_level"].ToString() == "2")
                        {
                            dgvr.Cells["问题级别名称2"].Value = "Critical";
                        }
                        else
                        {
                            dgvr.Cells["问题级别名称2"].Value = "";
                        }
                    }
                }
                label24.Text = cy.ToString();
                label27.Text = zy.ToString();
                label30.Text = yz.ToString();

                int hjbl = cy + zy + yz;//合计不良

                label14.Text = hjbl.ToString();
                #region Old Logic commented by Ashok on 20260121 to avoid Pivot88 Rjection while accepted in AEQS
                //if (hjbl > Convert.ToInt32(label12.Text))
                //{
                //    label15.Text = "Reject";
                //    label15.ForeColor = Color.Red;
                //}
                //else
                //{
                //    label15.Text = "Accepted";
                //    label15.ForeColor = Color.Green;
                //}
                #endregion
                #region New Logic  to check each criteria wise rejected qty insted of three criterias combined
                if ((Convert.ToInt32(label24.Text) >= Convert.ToInt32(label23.Text))|| (Convert.ToInt32(label27.Text) >= Convert.ToInt32(label26.Text))|| (Convert.ToInt32(label30.Text) >= Convert.ToInt32(label29.Text)))
                {
                    label15.Text = "Rejected";
                    label15.ForeColor = Color.Red;
                }
                else
                {
                    label15.Text = "Accepted";
                    label15.ForeColor = Color.Green;
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(CheckFinalTask())
            {
                DialogResult dr = MessageBox.Show("Are you sure to submit?!", "Submit", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                    EditAQLEntry_Sorting();
            }
           
        }

        private bool CheckFinalTask()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("task_no", dics["task_no"].ToString());
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                        "SJ_AQLAPI", "SJ_AQLAPI.F_AQL_Entry", "CheckFinalTask", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
            }
            return ret.IsSuccess;
        }

        /// <summary>
        /// 提交-AQL录入-不良排序
        /// </summary>
        public void EditAQLEntry_Sorting()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("task_no", dics["task_no"].ToString());
                data.Add("inspection_date", _uc.dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                data.Add("sample_level", comboBox1.SelectedValue.ToString());
                data.Add("aql_level", comboBox2.SelectedValue.ToString());
                data.Add("cma_task_br", GetDgvToTable(dataGridView4));
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.F_AQL_Entry", "EditAQLEntry_Sorting", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    dics["AQL_EDIT_STATE"] = "1";
                    p_frm.AQL_EDIT_STATE[0] = "1";
                    _uc.dateTimePicker1.Enabled = false;
                    DisabledEdit();
                    MessageBox.Show("提交成功!");
                    GetAQLEntry_RawLevel();

                    GetAQLEntry_Classify(string.Empty);

                    GetAQLEntry_Sorting();

                    //This feature needs to wait until AQL synchronization P88 is officially launched before it can be enabled. If you submit SVN, you need to comment it out.
                    TransferDataToPivot88();
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

        public void TakePh(DataGridViewCellEventArgs e)
        {
            var phRes = new FrmPhotographResult();
            FrmPhotograph frmTakePh = new FrmPhotograph(phRes);
            frmTakePh.ShowDialog();
            if (phRes.IsSuccess)
            {
                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, phRes.SaveImgPath, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    //label.Text = phRes.SaveImgName;
                    if (dataGridView4.Rows[e.RowIndex].Cells["imglist"].Value != null && !string.IsNullOrEmpty(dataGridView4.Rows[e.RowIndex].Cells["imglist"].Value.ToString()))
                    {
                        dataGridView4.Rows[e.RowIndex].Cells["imglist"].Value = dataGridView4.Rows[e.RowIndex].Cells["imglist"].Value + "," + resultDIC["guid"].ToString();
                    }
                    else
                    {
                        dataGridView4.Rows[e.RowIndex].Cells["imglist"].Value = resultDIC["guid"].ToString();
                    }
                    MessageBox.Show("提交成功！");
                    //string product_imgGuid1 = resultDIC["guid"].ToString();
                    //var webC = new System.Net.WebClient();
                    //string url = Program.Client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                    //Image image = new Bitmap(webC.OpenRead(url));
                    ////pictureBox.Image = image;
                    ////UploadInspection_GeneralInformationImg(product_imgGuid1);

                    //System.IO.File.Delete(phRes.SaveImgPath);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(phRes.ErrorMsg))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(phRes.ErrorMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    MessageBox.Show(phRes.ErrorMsg);
                }
            }
        }
        public void UploadInspection_GeneralInformationImg(string image_type, string image_index, string file_guid)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("task_no", dics["task_no"].ToString());
                data.Add("image_type", image_type);
                data.Add("image_index", image_index);
                data.Add("file_guid", file_guid);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_CmaTask_Photo", "UploadInspection_GeneralInformationImg", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("上传成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    MessageBox.Show(msg);
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

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //added for Pivot88

        /// <summary>
        /// 传输数据至pv88中间库
        /// </summary>
        public void TransferDataToPivot88()
        {
            string taskNo = dics["task_no"].ToString();
            Dictionary<string, object> dtoDic = new Dictionary<string, object>();

            try
            {
                // Is it the final task?
                if (!CheckIsFinalTask(taskNo))
                {
                    MessageBox.Show("This is not the final Task！");
                    return;
                }

                // Get test results
                Dictionary<string, object> checkResultDic = GetProductAndBoxCheckResult();
                Section_product product = (Section_product)checkResultDic["product"];
                Section_box box = (Section_box)checkResultDic["box"];

                AEQS_TO_P88_LIST aeqsMain = new AEQS_TO_P88_LIST();
                aeqsMain.status = "Submitted";
                aeqsMain.defective_parts = product.badQty;//产品不良数
                aeqsMain.passfails_0_title = "inspected_carton_numbers";
                aeqsMain.passfails_0_type = "list";
                aeqsMain.passfails_0_subsection = "actual_inspection";
                dtoDic.Add("aeqsMain", aeqsMain);


                //assignment task part
                AEQS_TO_P88_ASSIGNMENT aeqsAssignment = new AEQS_TO_P88_ASSIGNMENT();
                aeqsAssignment.assignment_items_inspection_result_id = product.checkResult;
                aeqsAssignment.assignment_items_inspection_status_id = aeqsAssignment.assignment_items_inspection_result_id == 1 ? 3 : 1;//合格传3，不合格传1
                aeqsAssignment.assignment_items_sampling_size = product.sample_qty; //(临时传的总双数，到后台计算)
                aeqsAssignment.assignment_items_sampled_inspected = dics["num"].ToInt();//(临时传的总样本，到后台计算)
                aeqsAssignment.assignment_items_aql_minor = 2.5m;
                aeqsAssignment.assignment_items_aql_major = 1.5m;
                aeqsAssignment.assignment_items_aql_major_a = 0;
                aeqsAssignment.assignment_items_aql_major_b = 0;
                aeqsAssignment.assignment_items_aql_critical = 0.01m;
                aeqsAssignment.assignment_items_assignment_report_type_id = 9;
                aeqsAssignment.assignment_items_assignment_inspection_level = GetSampLevelRomanNumber(comboBox1.SelectedValue.ToString());
                aeqsAssignment.assignment_items_assignment_inspection_method = "normal";
                aeqsAssignment.assignment_items_po_line_po_exporter_id = 233;//"219 APE 233 APC "
                aeqsAssignment.assignment_items_po_line_po_exporter_erp_business_id = "011";//"779 APE 011 APC 28I APH"
                aeqsAssignment.assignment_items_po_line_importer_id = 215;
                aeqsAssignment.assignment_items_po_line_importer_erp_business_id = "Adidas001";
                aeqsAssignment.assignment_items_po_line_importer_project_id = 2062;
                dtoDic.Add("aeqsAssignment", aeqsAssignment);


                //Packaging - header information
                AEQS_TO_P88_SECTIONS sectionBox = new AEQS_TO_P88_SECTIONS();
                sectionBox.sections_type = "aqlDefects";
                sectionBox.sections_title = "packing_packaging_labelling";
                sectionBox.sections_result_id = box.checkResult;
                sectionBox.sections_qty_inspected = dics["num"].ToInt();//分批数量
                sectionBox.sections_sampled_inspected = label8.Text.ToInt();//包装-样本数量
                sectionBox.sections_defective_parts = box.badQty; //包装的不良数
                sectionBox.sections_inspection_level = GetSampLevelRomanNumber(comboBox1.SelectedValue.ToString().Trim());
                sectionBox.sections_inspection_method = "normal";
                sectionBox.sections_aql_minor = 2.5m;
                sectionBox.sections_aql_major = 1.5m;
                sectionBox.sections_aql_critical = 0.010m;
                sectionBox.sections_qty_type = "carton";
                sectionBox.sections_max_minor_defects = box.minnor_accept;
                sectionBox.sections_max_major_defects = box.major_accept;
                sectionBox.sections_max_major_a_defects = 0;
                sectionBox.sections_max_major_b_defects = 0;
                sectionBox.sections_max_critical_defects = box.critical_accept;
                //sectionBox.sections_defects_critical_level = box.critical_actual;//严重数量
                //sectionBox.sections_defects_major_level = box.major_actual;
                //sectionBox.sections_defects_minor_level = box.minnor_actual;

                //Product-Header Information
                AEQS_TO_P88_SECTIONS sectionProduct = new AEQS_TO_P88_SECTIONS();
                sectionProduct.sections_type = "aqlDefects";
                sectionProduct.sections_title = "product";
                sectionProduct.sections_result_id = product.checkResult;
                sectionProduct.sections_defective_parts = product.badQty;
                sectionProduct.sections_qty_inspected = dics["num"].ToInt();
                sectionProduct.sections_sampled_inspected = product.sample_qty;
                sectionProduct.sections_inspection_level = GetSampLevelRomanNumber(comboBox1.SelectedValue.ToString().Trim());
                sectionProduct.sections_inspection_method = "normal";
                sectionProduct.sections_aql_minor = 2.5m;
                sectionProduct.sections_aql_major = 1.5m;
                sectionProduct.sections_aql_critical = 0.010m;
                sectionProduct.sections_max_minor_defects = product.minnor_accept;//次要允收数量
                sectionProduct.sections_max_major_defects = product.major_accept;
                sectionProduct.sections_max_major_a_defects = 0;
                sectionProduct.sections_max_major_b_defects = 0;
                sectionProduct.sections_max_critical_defects = product.critical_accept;
                //sectionProduct.sections_defects_critical_level = product.critical_actual;//严重数量
                //sectionProduct.sections_defects_major_level = product.major_actual;//主要数量
                //sectionProduct.sections_defects_minor_level = product.minnor_actual;


                List<object> boxList = new List<object>();
                List<object> productList = new List<object>();
                if (dataGridView4.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in dataGridView4.Rows)
                    {
                        if (dr.Cells["不良分类代号3"].Value != null && !string.IsNullOrEmpty(dr.Cells["不良分类代号3"].Value.ToString()))
                        {
                            if (dr.Cells["不良分类代号3"].Value.ToString().Equals("100"))
                            {
                                AEQS_TO_P88_SECTIONS subBox = sectionBox.CloneModel();
                                subBox.sections_defects_label = dr.Cells["不良项目名称2"].Value.ToString();
                               // subBox.sections_defects_code = "FTW" + dr.Cells["不良项目代号2"].Value.ToString();//  Commented to Remove FTW in new defect Codes on 2026/03/13
                                subBox.sections_defects_code =dr.Cells["不良项目代号2"].Value.ToString();
                                subBox.defect_image = dr.Cells["imglist"].Value.ToString();
                                string retdata3 = WebAPIHelper.Post(Program.Client.APIURL, "SJ_AQLAPI", "SJ_AQLAPI.F_AQL_Entry", "GetClassifyNameByBadItemCode", Program.Client.UserToken, JsonConvert.SerializeObject(dr.Cells["不良项目代号2"].Value.ToString()));
                                ResultObject ret3 = JsonConvert.DeserializeObject<ResultObject>(retdata3);
                                if (ret3.IsSuccess)
                                {
                                    subBox.sections_defects_subsection = ret3.RetData.ToString();
                                }
                                //Number of packaging issue levels
                                switch (dr.Cells["问题级别2"].Value.ToString())
                                {
                                    //main
                                    case "0":
                                        subBox.sections_defects_major_level = dr.Cells["不良数量"].Value.ToInt();//主要不良数
                                        break;
                                    //secondary
                                    case "1":
                                        subBox.sections_defects_minor_level = dr.Cells["不良数量"].Value.ToInt();//主要不良数
                                        break;
                                    //serious
                                    case "2":
                                        subBox.sections_defects_critical_level = dr.Cells["不良数量"].Value.ToInt();//严重不良数
                                        break;
                                    default:
                                        break;
                                }
                                boxList.Add(subBox);
                            }
                            else
                            {
                                AEQS_TO_P88_SECTIONS subProduct = sectionProduct.CloneModel();
                                subProduct.sections_defects_label = dr.Cells["不良项目名称2"].Value.ToString();
                                // subProduct.sections_defects_code = "FTW" + dr.Cells["不良项目代号2"].Value.ToString();//  Commented to Remove FTW in new defect Codes on 2026/03/13
                                subProduct.sections_defects_code = dr.Cells["不良项目代号2"].Value.ToString();
                                subProduct.defect_image = dr.Cells["imglist"].Value.ToString();

                                string retdata3 = WebAPIHelper.Post(Program.Client.APIURL, "SJ_AQLAPI", "SJ_AQLAPI.F_AQL_Entry", "GetClassifyNameByBadItemCode", Program.Client.UserToken, JsonConvert.SerializeObject(dr.Cells["不良项目代号2"].Value.ToString()));
                                ResultObject ret3 = JsonConvert.DeserializeObject<ResultObject>(retdata3);
                                if (ret3.IsSuccess)
                                {
                                    subProduct.sections_defects_subsection = ret3.RetData.ToString();
                                }
                                //Number of product issue levels
                                switch (dr.Cells["问题级别2"].Value.ToString())
                                {
                                    //main
                                    case "0":
                                        subProduct.sections_defects_major_level = dr.Cells["不良数量"].Value.ToInt();//主要不良数
                                        break;
                                    //secondary
                                    case "1":
                                        subProduct.sections_defects_minor_level = dr.Cells["不良数量"].Value.ToInt();//主要不良数
                                        break;
                                    //serious
                                    case "2":
                                        subProduct.sections_defects_critical_level = dr.Cells["不良数量"].Value.ToInt();//严重不良数
                                        break;
                                    default:
                                        break;
                                }
                                productList.Add(subProduct);
                            }
                        }

                    }
                }
                //If there are no bad items, only the header information will be transmitted.
                if (boxList.Count <= 0)
                {
                    boxList.Add(sectionBox);
                }
                if (productList.Count <= 0)
                {
                    productList.Add(sectionProduct);
                }
                dtoDic.Add("box", boxList);
                dtoDic.Add("product", productList);
                dtoDic.Add("task_no", taskNo);

                //Start syncing
                string retdata2 = WebAPIHelper.Post(Program.Client.APIURL, "SJ_AQLAPI", "SJ_AQLAPI.F_AQL_Entry", "TransferDataToPivot88",
                                                 Program.Client.UserToken, JsonConvert.SerializeObject(dtoDic));
                ResultObject ret2 = JsonConvert.DeserializeObject<ResultObject>(retdata2);
                if (ret2.IsSuccess)
                {
                    MessageBox.Show("Synchronized to p88 successfully！");
                }
                else
                {
                    MessageBox.Show(ret2.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Synchronization to p88 failed:" + ex.Message);
            }


        }


        /// <summary>
        /// 检查 Pivot88项目核对、点箱、照片是否已填
        /// </summary>
        /// <returns></returns>
        public bool CheckRequiredField(string taskNo)
        {
            List<string> checkList = new List<string>();
            checkList.Add(Atype.typekey5);
            checkList.Add(Atype.typekey6);
            //checkList.Add(Atype.typekey8);
            checkList.Add(Atype.typekey25);//cspia新枚举值
            checkList.Add(Atype.typekey12);
            checkList.Add(Atype.typekey13);
            checkList.Add(Atype.typekey10);

            checkList.Add(Atype.typekey14);
            checkList.Add(Atype.typekey15);

            checkList.Add(Atype.typekey17);
            checkList.Add(Atype.typekey18);
            checkList.Add(Atype.typekey23);
            checkList.Add(Atype.typekey24);
            checkList.Add(Atype.typekey19);
            checkList.Add(Atype.typekey20);
            checkList.Add(Atype.typekey21);

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("task_no", taskNo);
            p.Add("checkList", checkList);
            string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_AQLAPI", "SJ_AQLAPI.F_AQL_Entry", "CheckRequiredField",
                                               Program.Client.UserToken, JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否为最终任务
        /// </summary>
        /// <returns></returns>
        public bool CheckIsFinalTask(string taskNo)
        {
            string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_AQLAPI", "SJ_AQLAPI.F_AQL_Entry", "CheckIsFinalTask",
                                             Program.Client.UserToken, JsonConvert.SerializeObject(taskNo));
            ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取否定项数量
        /// </summary>
        /// <returns></returns>
        public bool GetNegativeItemNums(string taskNo)
        {
            //string AQLResult = string.Empty;
            Dictionary<string, object> p = new Dictionary<string, object>();

            List<string> typeList = new List<string>();
            typeList.Add(Atype.typekey12);
            typeList.Add(Atype.typekey13);
            typeList.Add(Atype.typekey5);
            typeList.Add(Atype.typekey8);
            typeList.Add(Atype.typekey10);
            typeList.Add(Atype.typekey14);

            typeList.Add(Atype.typekey15);
            typeList.Add(Atype.typekey6);
            typeList.Add(Atype.typekey23);
            typeList.Add(Atype.typekey24);
            typeList.Add(Atype.typekey19);
            typeList.Add(Atype.typekey20);
            typeList.Add(Atype.typekey21);

            p.Add("task_no", taskNo);
            p.Add("typeList", typeList);
            string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_AQLAPI", "SJ_AQLAPI.F_AQL_Entry", "GetNegativeItemNums",
                                               Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                return false;
            }
            //PV88核对项目有否的项目有一个为否，则不通过。
            int count = JsonConvert.DeserializeObject<int>(ret.RetData);
            if (count > 0)
            {
                return false;//Reject
            }
            return true;
        }

        /// <summary>
        /// 获取包装，产品两类问题的检验结果，根据这两个得出总结果
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetProductAndBoxCheckResult()
        {
            Dictionary<string, object> checkResultDic = new Dictionary<string, object>();
            try
            {
                //产品
                Section_product product = new Section_product();
                product.major_accept = label25.Text.ToInt();
                product.major_reject = label26.Text.ToInt();
                product.minnor_accept = label22.Text.ToInt();
                product.minnor_reject = label23.Text.ToInt();
                product.critical_accept = label28.Text.ToInt();
                product.critical_reject = label29.Text.ToInt();
                product.sample_qty = label8.Text.ToInt();

                int box_major_actual = 0;
                int box_minnor_actual = 0;
                int box_critical_actual = 0;

                if (dataGridView4.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in dataGridView4.Rows)
                    {
                        switch (dr.Cells["问题级别2"].Value)
                        {
                            case "0": //主要
                                if (dr.Cells["不良分类代号3"].Value.ToString().Equals("100")) //包装
                                {
                                    box_major_actual += Convert.ToInt32(dr.Cells["不良数量"].Value.ToString());
                                }
                                else
                                {
                                    product.major_actual += Convert.ToInt32(dr.Cells["不良数量"].Value.ToString());
                                }
                                break;
                            case "1"://次要
                                if (dr.Cells["不良分类代号3"].Value.ToString().Equals("100")) //包装
                                {
                                    box_minnor_actual += Convert.ToInt32(dr.Cells["不良数量"].Value.ToString());
                                }
                                else
                                {
                                    product.minnor_actual += Convert.ToInt32(dr.Cells["不良数量"].Value.ToString());
                                }
                                break;
                            case "2"://严重
                                if (dr.Cells["不良分类代号3"].Value.ToString().Equals("100")) //包装
                                {
                                    box_critical_actual += Convert.ToInt32(dr.Cells["不良数量"].Value.ToString());
                                }
                                else
                                {
                                    product.critical_actual += Convert.ToInt32(dr.Cells["不良数量"].Value.ToString());
                                }
                                break;
                        }
                    }
                }

                product.badQty = product.major_actual + product.minnor_actual + product.critical_actual;
                if (product.major_actual >= product.major_reject || product.minnor_actual >= product.minnor_reject || product.critical_actual >= product.critical_reject)
                {
                    product.checkResult = 2;//Rejct
                }
                else
                {
                    product.checkResult = 1;//Accept
                }


                //包装（根据产品的样本数量再次抽样，然后得出三个级别的数量）
                //包装
                Section_box box = new Section_box();
                box.major_actual = box_major_actual;
                box.minnor_actual = box_minnor_actual;
                box.critical_actual = box_critical_actual;
                box.box_qty = product.sample_qty;
                box.badQty = box.major_actual + box.minnor_actual + box.critical_actual;

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("ac", comboBox2.SelectedValue.ToString());//抽样标准
                //p.Add("num", product.sample_qty);//
                p.Add("num", dics["num"].ToInt());
                p.Add("LEVEL_TYPE", comboBox1.SelectedValue.ToString());//样品级别
                p.Add("task_no", dics["task_no"]);
                p.Add("po", dics["po"]);
                p.Add("task_type", dics["task_type"]);
                string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_AQLAPI", "SJ_AQLAPI.F_AQL_Entry", "GetAQLEntry_SamplingRate",
                                            Program.Client.UserToken, JsonConvert.SerializeObject(p));
                ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                var dt1213 = JsonConvert.DeserializeObject<DataTable>(dic["Data1213"].ToString());
                var Datadx = JsonConvert.DeserializeObject<DataTable>(dic["Datadx"].ToString());
                if (dt1213.Rows.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(dt1213.Rows[0]["VALS"].ToString()))
                    {
                        int ac12 = Convert.ToInt32(dt1213.Rows[0]["AC12"].ToString());//1.5 主要
                        int ac13 = Convert.ToInt32(dt1213.Rows[0]["AC13"].ToString());//2.5 次要
                        box.minnor_accept = ac13; //次要-允收
                        box.minnor_reject = ac13 + 1;
                        box.major_accept = ac12;//主要-允收
                        box.major_reject = ac12 + 1;

                        box.critical_accept = label28.Text.ToInt();//严重-允收
                        box.critical_reject = label29.Text.ToInt();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["VALS"].ToString()))
                    {
                        decimal VALS = Convert.ToDecimal(dt.Rows[0]["VALS"].ToString());//样本数量
                        box.box_qty = product.sample_qty;
                        box.sample_qty = VALS.ToInt();
                    }
                }
                if (box.major_actual >= box.major_reject || box.minnor_actual >= box.minnor_reject || box.critical_actual >= box.critical_reject)
                {
                    box.checkResult = 2;//Rejct
                }
                else
                {
                    box.checkResult = 1;//Accept
                }


                //总检验结果,必须包装通过，产品通过，否定项通过才通过。
                int result = 0;
                if (box.checkResult == 1 && product.checkResult == 1 && GetNegativeItemNums(dics["task_no"].ToString()))
                {
                    result = 1;
                }
                else
                {
                    result = 2;
                }

                checkResultDic.Add("product", product);
                checkResultDic.Add("box", box);
                checkResultDic.Add("result", result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return checkResultDic;
        }

        /// <summary>
        /// 获得样本级别（检验等级）的罗马数字
        /// </summary>
        public string GetSampLevelRomanNumber(string sampleLevel)
        {
            string number = string.Empty;
            switch (sampleLevel)
            {
                case "1":
                    number = "I";
                    break;
                case "2":
                    number = "Ⅱ";
                    break;
                case "3":
                    number = "Ⅲ";
                    break;
                case "4":
                    number = "S-1";
                    break;
                case "5":
                    number = "S-2";
                    break;
                case "6":
                    number = "S-3";
                    break;
                case "7":
                    number = "S-4";
                    break;
            }
            return number;
        }

    }


    class Section_product
    {

        //主要
        public int major_accept { get; set; }
        public int major_reject { get; set; }
        public int major_actual { get; set; }

        //次要
        public int minnor_accept { get; set; }
        public int minnor_reject { get; set; }
        public int minnor_actual { get; set; }

        //严重
        public int critical_accept { get; set; }
        public int critical_reject { get; set; }
        public int critical_actual { get; set; }


        public int checkResult { get; set; }
        public int sample_qty { get; set; }
        public int badQty { get; set; }

    }

    class Section_box
    {
        //主要
        public int major_accept { get; set; }
        public int major_reject { get; set; }
        public int major_actual { get; set; }

        //次要
        public int minnor_accept { get; set; }
        public int minnor_reject { get; set; }
        public int minnor_actual { get; set; }

        //严重
        public int critical_accept { get; set; }
        public int critical_reject { get; set; }
        public int critical_actual { get; set; }


        public int checkResult { get; set; }
        public int box_qty { get; set; }//纸箱数量
        public int sample_qty { get; set; }
        public int badQty { get; set; }

    }
}

