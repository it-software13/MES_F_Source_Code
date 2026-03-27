using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.WebAPI;
using SjeMES_QCM_Ex;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_IQC
{
    public partial class F_IQC_Customer_Complaint_Dispose : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string COMPLAINT_NO = string.Empty;//投诉编号
        string imglist = string.Empty;//投诉所有文件
        string fxfile = string.Empty;//分析文件
        string zrpdfile = string.Empty;//责任判定文件
        string gscsfile = string.Empty;//改善措施文件
        string shoe_no = string.Empty;//鞋型
        string ART = string.Empty;//鞋型
        string PO = string.Empty;//PO号
        public F_IQC_Customer_Complaint_Dispose(string _COMPLAINT_NO,string _state,string _ART,string _PO)
        {
            InitializeComponent();
            COMPLAINT_NO = _COMPLAINT_NO;
            ART = _ART;
            PO = _PO;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            if (_state == "Closed")
                StateEnabled();
        }
        public F_IQC_Customer_Complaint_Dispose(string _COMPLAINT_NO, string _state, string _ART, string _PO, SJeMES_Framework.Class.ClientClass client)
        {
            Program.Client = client;
            InitializeComponent();
            COMPLAINT_NO = _COMPLAINT_NO;
            ART = _ART;
            PO = _PO;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            if (_state == "Closed")
                StateEnabled();
        }


        /// <summary>
        /// 初始化控件赋空
        /// </summary>
        public void empty()
        {
            #region 投诉信息
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";
            lbl_fob.Text = "";
            #endregion

            #region 鞋型信息
            label22.Text = "";
            label23.Text = "";
            label24.Text = "";
            label25.Text = "";
            label26.Text = "";
            label27.Text = "";
            label28.Text = "";
            label29.Text = "";
            #endregion

            #region 订单信息
            label36.Text = "";
            label37.Text = "";
            label38.Text = "";
            label39.Text = "";
            label40.Text = "";
            label41.Text = "";
            #endregion

            #region 品质成本
            label45.Text = "";
            #endregion
        }

        /// <summary>
        /// 状态为结案禁用
        /// </summary>
        public void StateEnabled()
        {
            button9.Enabled = false;
            button11.Enabled = false;
            button14.Enabled = false;
            button12.Enabled = false;
            button17.Enabled = false;
            button15.Enabled = false;
            button18.Enabled = false;
            button19.Enabled = false;
            button20.Enabled = false;
            button21.Enabled = false;

            dataGridView1.Columns["操作"].Visible = false;
        }

        /// <summary>
        /// 客户投诉处理时查询
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetCustomer_Complaint_Dispose()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("COMPLAINT_NO", COMPLAINT_NO);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_Customer_Complaint",//类名
                                            "GetCustomer_Complaint_Dispose",//方法名
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
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());//投诉信息
                if (dt.Rows.Count > 0)
                {
                    label8.Text = dt.Rows[0]["COMPLAINT_NO"].ToString();
                    lbl_fob.Text = dt.Rows[0]["FOB"].ToString();
                    label9.Text = dt.Rows[0]["COMPLAINT_DATE"].ToString();
                    label10.Text = dt.Rows[0]["PO_ORDER"].ToString();
                    label11.Text = dt.Rows[0]["COUNTRY_REGION"].ToString();
                    label12.Text = dt.Rows[0]["NG_QTY"].ToString();
                    label13.Text = dt.Rows[0]["COMPLAINT_MONEY"].ToString();
                    richTextBox1.Text = dt.Rows[0]["DEFECT_CONTENT"].ToString();

                    if (dt.Rows[0]["processing_results_status"].ToString() == "0")
                        radioButton1.Checked = true;
                    else if (dt.Rows[0]["processing_results_status"].ToString() == "1")
                        radioButton2.Checked = true;
                    else if (dt.Rows[0]["processing_results_status"].ToString() == "2")
                        radioButton3.Checked = true;

                    imglist = dt.Rows[0]["imglist"].ToString();
                    fxfile = dt.Rows[0]["fxfile"].ToString();
                    zrpdfile = dt.Rows[0]["zrpbfile"].ToString();
                    gscsfile = dt.Rows[0]["gscsfile"].ToString();

                    label36.Text = dt.Rows[0]["PO_ORDER"].ToString();
                    label38.Text = dt.Rows[0]["cx"].ToString();
                    label39.Text = dt.Rows[0]["SE_QTY"].ToString();
                    label40.Text = dt.Rows[0]["SE_YEAR"].ToString();

                    richTextBox2.Text = dt.Rows[0]["analysis"].ToString();
                    richTextBox3.Text = dt.Rows[0]["liability_determination"].ToString();
                    richTextBox4.Text = dt.Rows[0]["improvement_measures"].ToString();
                    richTextBox5.Text = dt.Rows[0]["processing_results"].ToString();
                    if (dt.Rows[0]["processing_results_status"].ToString() == "0")
                        radioButton1.Checked = true;
                    else if (dt.Rows[0]["processing_results_status"].ToString() == "1")
                        radioButton2.Checked = true;
                    else if (dt.Rows[0]["processing_results_status"].ToString() == "2")
                        radioButton3.Checked = true;

                }

                var dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());//鞋型信息
                if (dt1.Rows.Count > 0)
                {
                    label22.Text = dt1.Rows[0]["DEVELOP_SEASON"].ToString();
                    label23.Text = dt1.Rows[0]["Material_Way"].ToString();
                    label24.Text = dt1.Rows[0]["user_section"].ToString();
                    label25.Text = dt1.Rows[0]["cwa_date"].ToString();
                    label26.Text = dt1.Rows[0]["PROD_NO"].ToString();
                    label27.Text = dt1.Rows[0]["rule_no"].ToString();
                    label28.Text = dt1.Rows[0]["qa_principal"].ToString();
                    label29.Text = dt1.Rows[0]["name_t"].ToString();
                    shoe_no = dt1.Rows[0]["SHOE_NO"].ToString();

                    if (!string.IsNullOrEmpty(dt1.Rows[0]["file_url"].ToString()))
                    {
                        try
                        {
                            var webC = new System.Net.WebClient();
                            string url = Program.Client.PicUrl + Convert.ToString(dt1.Rows[0]["file_url"].ToString());
                            Image image = new Bitmap(webC.OpenRead(url));
                            pictureBox1.Image = image;
                        }
                        catch
                        {
                        }
                    }
                }

                var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data3"].ToString());//品质成本
                dataGridView1.Rows.Clear();
                if (dt2.Rows.Count > 0)
                {
                    int a = 0;
                    foreach (DataRow dr in dt2.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[a];
                        dgvr.Cells["异常成本类别"].Value = dr["qa_cost_cate_name"].ToString();
                        dgvr.Cells["参考单价"].Value = dr["ref_unit_price"].ToString();
                        dgvr.Cells["实际单价"].Value = dr["act_unit_price"].ToString();
                        dgvr.Cells["单位"].Value = dr["qa_cost_cate_u"].ToString();
                        dgvr.Cells["数量"].Value = dr["quantity"].ToString();
                        dgvr.Cells["异常成本类别编号"].Value = dr["qa_cost_cate_no"].ToString();
                        a++;
                    }
                    decimal sumnum = 0;//总计
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["实际单价"].Value.ToString()) && !string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["数量"].Value.ToString()))
                        {
                            decimal price = Convert.ToDecimal(dataGridView1.Rows[i].Cells["实际单价"].Value.ToString());
                            decimal num = Convert.ToDecimal(dataGridView1.Rows[i].Cells["数量"].Value.ToString());
                            sumnum += (price * num);
                            dataGridView1.Rows[i].Cells["合计成本"].Value = (price * num).ToString();
                        }
                    }
                    label45.Text = sumnum.ToString();
                }
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void F_IQC_Customer_Complaint_Dispose_Load(object sender, EventArgs e)
        {
            empty();

            GetCustomer_Complaint_Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var currRowFileDt = Getimage_guid(imglist);
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
            imglist = image_guids;
        }

        //查看DQA文件
        private void button2_Click(object sender, EventArgs e)
        {
            FrmFileList add = new FrmFileList(FileView(), Program.Client.UploadUrl, Program.Client.UserToken,"",true,false);
            add.ShowDialog();
        }

        /// <summary>
        /// 查看文件;
        /// </summary>
        public DataTable FileView()
        {
            DataTable dt = new DataTable();
            try
            {
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("SHOE_NO", shoe_no);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.DQA_ShoeShape",//类名
                                            "GetDQAtraitMainFile",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
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


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 客户投诉编辑页面查询图片
        /// </summary>
        /// <returns></returns>
        public static DataTable Getimage_guid(string image_guid)
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("image_guid", image_guid);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.IQC_Customer_Complaint",//类名
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

        //查看分析文件
        private void button10_Click(object sender, EventArgs e)
        {
            var currRowFileDt = Getimage_guid(fxfile);
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
            fxfile = image_guids;
        }

        //查看责任判定文件
        private void button13_Click(object sender, EventArgs e)
        {
            var currRowFileDt = Getimage_guid(zrpdfile);
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
            zrpdfile = image_guids;
        }

        //查看改善措施文件
        private void button16_Click(object sender, EventArgs e)
        {
            var currRowFileDt = Getimage_guid(gscsfile);
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
            gscsfile = image_guids;
        }

        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        //分析文件上传
        private void button9_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "Please select a folder";
            ofd.Filter = "All files|*.*";
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
                        if (fxfile != null && !string.IsNullOrEmpty(fxfile))
                        {
                            fxfile = fxfile + "," + resultDIC["guid"].ToString();
                        }
                        else
                        {
                            fxfile = resultDIC["guid"].ToString();
                        }
                        //string union_id = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                        //TQC_Task_Edit_Upload(union_id, resultDIC["guid"].ToString());
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("uploaded successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    }
                }

            }
        }

        //责任判定文件上传
        private void button14_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "Please select a folder";
            ofd.Filter = "All files|*.*";
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
                        if (zrpdfile != null && !string.IsNullOrEmpty(zrpdfile))
                        {
                            zrpdfile = zrpdfile + "," + resultDIC["guid"].ToString();
                        }
                        else
                        {
                            zrpdfile = resultDIC["guid"].ToString();
                        }
                        //string union_id = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                        //TQC_Task_Edit_Upload(union_id, resultDIC["guid"].ToString());
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("uploaded successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    }
                }

            }
        }

        //改善措施文件上传
        private void button17_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "Please select a folder";
            ofd.Filter = "All files|*.*";
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
                        if (gscsfile != null && !string.IsNullOrEmpty(gscsfile))
                        {
                            gscsfile = gscsfile + "," + resultDIC["guid"].ToString();
                        }
                        else
                        {
                            gscsfile = resultDIC["guid"].ToString();
                        }
                        //string union_id = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                        //TQC_Task_Edit_Upload(union_id, resultDIC["guid"].ToString());
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("uploaded successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    }
                }

            }
        }

        /// <summary>
        /// 客户投诉处理页面保存
        /// </summary>
        public void EditCustomer_Complaint_Dispose(string file_type, string ali_remarks, string ali_img)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("COMPLAINT_NO", COMPLAINT_NO);//条件 投诉编号
                data.Add("file_type", file_type);//条件 文件类型
                data.Add("ali_remarks", ali_remarks);//条件 分析/责任判定/改善措施的信息
                data.Add("ali_img", ali_img);//条件 图片guid集
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJeMES_IQC", "SJeMES_IQC.IQC_Customer_Complaint", "EditCustomer_Complaint_Dispose", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);

                if (ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                }
                else
                    throw new Exception(ret.ErrMsg.ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            EditCustomer_Complaint_Dispose("1", richTextBox2.Text.Trim(), fxfile);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            EditCustomer_Complaint_Dispose("2", richTextBox3.Text.Trim(), zrpdfile);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            EditCustomer_Complaint_Dispose("3", richTextBox4.Text.Trim(), gscsfile);
        }

        /// <summary>
        /// 客户投诉处理页面改善结果保存
        /// </summary>
        public void EditCustomer_Complaint_Dispose_gsjg()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("COMPLAINT_NO", COMPLAINT_NO);//条件 投诉编号
                data.Add("processing_results", richTextBox5.Text.Trim());//条件 处理结果
                if (radioButton1.Checked)
                    data.Add("processing_results_status", "0");//条件 处理结果状态
                else if (radioButton2.Checked)
                    data.Add("processing_results_status", "1");//条件 处理结果状态
                else if (radioButton3.Checked)
                    data.Add("processing_results_status", "2");//条件 处理结果状态
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJeMES_IQC", "SJeMES_IQC.IQC_Customer_Complaint", "EditCustomer_Complaint_Dispose_gsjg", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);

                if (ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                }
                else
                    throw new Exception(ret.ErrMsg.ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            EditCustomer_Complaint_Dispose_gsjg();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int index = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells["异常成本类别编号"].Value = "";
            this.dataGridView1.Rows[index].Cells["异常成本类别"].Value = "";
            this.dataGridView1.Rows[index].Cells["参考单价"].Value = "";
            this.dataGridView1.Rows[index].Cells["实际单价"].Value = "";
            this.dataGridView1.Rows[index].Cells["单位"].Value = "";
            this.dataGridView1.Rows[index].Cells["数量"].Value = "";
            this.dataGridView1.Rows[index].Cells["合计成本"].Value = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "异常成本类别")
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    DataTable dt_tval = GetCustomer_Complaint_Dispose_qa_cost_cate();
                    comboBox1.DataSource = dt_tval;
                    if (dt_tval != null && dt_tval.Rows.Count > 0)
                    {
                        comboBox1.DisplayMember = "value";
                        comboBox1.ValueMember = "code";
                    }
                    string 异常成本类别 = dataGridView1.CurrentRow.Cells["异常成本类别"].Value.ToString(); //对combobox赋值
                    comboBox1.Text = 异常成本类别;

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    comboBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    comboBox1.Visible = true;
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "实际单价") // 实际单价
                {
                    textBox2.Visible = false;
                    comboBox1.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["实际单价"].Value is null ? "" : dataGridView1.CurrentRow.Cells["实际单价"].Value.ToString();
                    string measures = aa == "" ? "" : aa;
                    textBox1.Text = measures; //实际单价

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox1.Visible = true;
                    textBox1.Focus();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "数量") // 数量
                {
                    textBox1.Visible = false;
                    comboBox1.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["数量"].Value is null ? "" : dataGridView1.CurrentRow.Cells["数量"].Value.ToString();
                    string measures = aa == "" ? "" : aa;
                    textBox2.Text = measures; //数量

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox2.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox2.Visible = true;
                    textBox2.Focus();
                }
                else
                {
                    comboBox1.Visible = false;
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "操作")
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    comboBox1.Visible = false;
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["操作"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("删除"))
                    {
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
        }

        /// <summary>
        /// 客户投诉处理页面查询异常成本类别
        /// </summary>
        /// <returns></returns>
        public DataTable GetCustomer_Complaint_Dispose_qa_cost_cate()
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.IQC_Customer_Complaint",//类名
                                        "GetCustomer_Complaint_Dispose_qa_cost_cate",//方法名
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
            return dt;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = textBox1.Text.ToString();
            decimal sumnum = 0;//总计
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["实际单价"].Value.ToString()) && !string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["数量"].Value.ToString()))
                {
                    decimal price = Convert.ToDecimal(dataGridView1.Rows[i].Cells["实际单价"].Value.ToString());
                    decimal num = Convert.ToDecimal(dataGridView1.Rows[i].Cells["数量"].Value.ToString());
                    sumnum += (price * num);
                    dataGridView1.Rows[i].Cells["合计成本"].Value = (price * num).ToString();
                }
            }
            label45.Text = sumnum.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = textBox2.Text.ToString();
            decimal sumnum = 0;//总计
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["实际单价"].Value.ToString()) && !string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["数量"].Value.ToString()))
                {
                    decimal price = Convert.ToDecimal(dataGridView1.Rows[i].Cells["实际单价"].Value.ToString());
                    decimal num = Convert.ToDecimal(dataGridView1.Rows[i].Cells["数量"].Value.ToString());
                    sumnum += (price * num);
                    dataGridView1.Rows[i].Cells["合计成本"].Value = (price * num).ToString();
                }
            }
            label45.Text = sumnum.ToString();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("qa_cost_cate_no",comboBox1.SelectedValue.ToString());
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.IQC_Customer_Complaint",//类名
                                        "GetCustomer_Complaint_Dispose_qa_cost_cate_no",//方法名
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
            dataGridView1.CurrentRow.Cells["异常成本类别编号"].Value = dt.Rows[0]["qa_cost_cate_no"].ToString();
            dataGridView1.CurrentRow.Cells["异常成本类别"].Value = dt.Rows[0]["qa_cost_cate_name"].ToString();
            dataGridView1.CurrentRow.Cells["参考单价"].Value = dt.Rows[0]["unit_price"].ToString();
            dataGridView1.CurrentRow.Cells["实际单价"].Value = dt.Rows[0]["unit_price"].ToString();
            dataGridView1.CurrentRow.Cells["单位"].Value = dt.Rows[0]["qa_cost_cate_u"].ToString();
            decimal sumnum = 0;//总计
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["实际单价"].Value.ToString())&& !string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["数量"].Value.ToString()))
                {
                    decimal price = Convert.ToDecimal(dataGridView1.Rows[i].Cells["实际单价"].Value.ToString());
                    decimal num= Convert.ToDecimal(dataGridView1.Rows[i].Cells["数量"].Value.ToString());
                    sumnum += (price * num);
                    dataGridView1.Rows[i].Cells["合计成本"].Value = (price*num).ToString();
                }
            }
            label45.Text = sumnum.ToString();

            comboBox1.Visible = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                e.Handled = true;  //非以上键则禁止输入
            }
            if (e.KeyChar == '0' && textBox1.Text.Trim() == "") e.Handled = true; //禁止第一个字符就输入0
            if (e.KeyChar == '.' && textBox1.Text.Trim() == "") e.Handled = true; //禁止第一个字符就输入小数点
            if (e.KeyChar == '.' && textBox1.Text.Contains(".")) e.Handled = true; //禁止输入多个小数点
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                e.Handled = true;  //非以上键则禁止输入
            }
            if (e.KeyChar == '0' && textBox2.Text.Trim() == "") e.Handled = true; //禁止第一个字符就输入0
            if (e.KeyChar == '.' && textBox2.Text.Trim() == "") e.Handled = true; //禁止第一个字符就输入小数点
            if (e.KeyChar == '.' && textBox2.Text.Contains(".")) e.Handled = true; //禁止输入多个小数点
        }

        /// <summary>
        /// 客户投诉处理页面品质成本保存
        /// </summary>
        public void EditCustomer_Complaint_Dispose_pzcb()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("COMPLAINT_NO", COMPLAINT_NO);//条件 投诉编号
                data.Add("CUSTOMER_COMPLAINT_M_C", GetDgvToTable(dataGridView1));//条件 客户投诉——品质成本类别
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJeMES_IQC", "SJeMES_IQC.IQC_Customer_Complaint", "EditCustomer_Complaint_Dispose_pzcb", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);

                if (ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                }
                else
                    throw new Exception(ret.ErrMsg.ToString());
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

        private void button20_Click(object sender, EventArgs e)
        {
            EditCustomer_Complaint_Dispose_pzcb();
        }

        /// <summary>
        /// 客户投诉处理页面结案
        /// </summary>
        public void EditCustomer_Complaint_Dispose_ja()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("COMPLAINT_NO", COMPLAINT_NO);//条件 投诉编号
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJeMES_IQC", "SJeMES_IQC.IQC_Customer_Complaint", "EditCustomer_Complaint_Dispose_ja", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);

                if (ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    this.Close();
                }
                else
                    throw new Exception(ret.ErrMsg.ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            EditCustomer_Complaint_Dispose_ja();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> showValues = new Dictionary<string, string>();
            showValues.Add("complaint_points", richTextBox1.Text);
            showValues.Add("shoe_name", label29.Text);
            showValues.Add("art", label26.Text);
            showValues.Add("production_plant", label37.Text);
            showValues.Add("line_type", label38.Text);
            showValues.Add("delivery_time", label41.Text);
            showValues.Add("imglist", imglist);
            F_IQC_Customer_Complaint_Dispose_Art_Detail frm = new F_IQC_Customer_Complaint_Dispose_Art_Detail(showValues);
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GetEx_LookResult();
        }

        /// <summary>
        /// 客户投诉处理页查询实验室测试报告
        /// </summary>
        public void GetEx_LookResult()
        {
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("ART", ART);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.IQC_Customer_Complaint",//类名
                                        "GetEx_LookResult",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                MessageBox.Show("Failed to get data");
                return;
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //视图数据显示
            //var info = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(dic["task_no"].ToString());
            if (string.IsNullOrWhiteSpace(dic["task_no"].ToString()))
            {
                MessageBox.Show("No lab test report!");
                return;
            }
            using (F_QCM_Ex_LookResult frm = new F_QCM_Ex_LookResult(dic["task_no"].ToString(),Program.Client))
            {
                frm.ShowDialog();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (F_IQC_Customer_Complaint_Dispose_Inspection f=new F_IQC_Customer_Complaint_Dispose_Inspection(PO))
            {
                f.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label47_Click(object sender, EventArgs e)
        {

        }
    }
}
