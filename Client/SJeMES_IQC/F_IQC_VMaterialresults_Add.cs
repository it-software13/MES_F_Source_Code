using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Control_Library.VideoCapture;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using SJeMES_IQC.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SJeMES_IQC
{
    public partial class F_IQC_VMaterialresults_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private Dictionary<string, object> dics;
        private List<imginfo> img_list = new List<imginfo>();
        public List<imginfo2> image_listimg = new List<imginfo2>();
        public F_IQC_VMaterialresults_Add(Dictionary<string, object> dic)
        {
            InitializeComponent();
            dics = dic;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
         Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                                e.RowBounds.Location.Y,
                                                dgv.RowHeadersWidth - 4,
                                                e.RowBounds.Height);


            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                                    dgv.RowHeadersDefaultCellStyle.Font,
                                    rectangle,
                                    dgv.RowHeadersDefaultCellStyle.ForeColor,
                                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);

        }

        private void F_QCM_VMaterialresults_Add_Load(object sender, EventArgs e)
        {
            lab_lh.Text = dics["ITEM_NO"].ToString();//料号
            lab_sccs.Text = dics["SUPPLIERS_NAME"].ToString();//生产厂商
            lab_jcsl.Text = dics["RCPT_QTY"].ToString();//进仓数量
            textBox2.Text = dics["SHOE_NO"].ToString();//鞋型
            textBox1.Text = dics["PROD_NO"].ToString();//art
            textBox3.Text = dics["PART_NO"].ToString();//部位
            textBox4.Text = dics["ITEM_NAME"].ToString();//物料名称
            txt_pass_qty.Text = dics["RCPT_QTY"].ToString();//合格数量
            lab_name.Text = "";//检验员
            lab_data.Text = "";//检验时间
            lab_data.Text = DateTime.Now.ToString("yyyy-MM-dd");

            //加载下拉框
            cbl_view();
            cbo_jysp.SelectedValue = 2;
            cbo_AQLjb.SelectedValue = 13;//初始的数据默认一般检验水平Ⅱ AQL级别2.5
            //加载表身视图
            Getlist();
            img_list = new List<imginfo>();
        }
        private int ids;
        /// <summary>
        /// 主数据视图
        /// </summary>
        private void Getlist()
        {

            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("CHK_NO", dics["CHK_NO"].ToString());//收料单号
                p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//材料序号
                p.Add("ITEM_NO", dics["ITEM_NO"].ToString());//料号
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.VMaterialinventory",//类名
                                            "CheckResultLRView",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                DataTable dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());//表头数据
                string addopn = dic["status"].ToString();//录入信息就开启按钮
                if (addopn == "0")
                {
                    btn_add.Enabled = false;//0结案不能提交
                }
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {

                    lab_jqty.Text = lab_jcsl.Text;//检验数
                    if (dt2.Rows.Count > 0)
                    {
                        if (dt2.Rows[0]["determine"].ToString() == "0")
                        {
                            lab_PF.Text = "PASS";
                            lab_PF.ForeColor = Color.Green;
                            radioButton_pass.Checked = true;
                        }
                        else
                        {
                            lab_PF.Text = "FAIL";
                            lab_PF.ForeColor = Color.Red;
                            radioButton_fail.Checked = true;
                        }
                        //ids = int.Parse(dt2.Rows[0]["id"].ToString());
                        cbo_jysp.SelectedValue = dt2.Rows[0]["testlevel"].ToString();//aql级别
                        cbo_AQLjb.SelectedValue = dt2.Rows[0]["aql_level"].ToString();//检验水平
                        txt_acre.Text = dt2.Rows[0]["ac_re"].ToString();//ac_re
                        txt_cysl.Text = dt2.Rows[0]["sample_qty"].ToString();//抽检数量
                        txt_pass_qty.Text = dt2.Rows[0]["PASS_QTY"].ToString();//合格数量
                        lab_data.Text = dt2.Rows[0]["INSPECTIONDATE"].ToString();//检验日期
                        txt_code.Text = dt2.Rows[0]["STAFF_NO"].ToString();//检验员编号
                        lab_name.Text = dt2.Rows[0]["STAFF_NAME"].ToString();//检验员名称
                        txt_remark.Text = dt2.Rows[0]["REMARK"].ToString();//备注
                        usercode = dt2.Rows[0]["STAFF_NO"].ToString();
                        string status = dt2.Rows[0]["CHK_NO"].ToString();
                        if (!string.IsNullOrWhiteSpace(status))
                        {
                            button1.Visible = true;// 
                            button1.Enabled = true;//要是存在就给取消
                        }


                    }

                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["test_item_no"].Value = dr["INSPECTION_CODE"].ToString();//检测项编号
                        dgvr.Cells["test_item_name"].Value = dr["INSPECTION_NAME"].ToString();//检测项名称
                        dgvr.Cells["badproblem_code"].Value = dr["BADPROBLEM_CODE"].ToString();//备注
                        dgvr.Cells["badproblem_name"].Value = dr["REMARKS"].ToString();//备注
                        dgvr.Cells["test_standard"].Value = dr["INSPECTION_STANDARD"].ToString();//检测项标准
                        dgvr.Cells["CHK_NO"].Value = dr["CHK_NO"].ToString();//收料单号
                        dgvr.Cells["id"].Value = dr["ID"].ToString();//ID
                                                                     //dgvr.Cells["DETERMINE"].Value = "是";
                        dgvr.Cells["DETERMINE"].Value = "Yes";
                        if (dr["DETERMINE"].ToString() == "0")
                        {
                            //dgvr.Cells["DETERMINE"].Value = "是";
                            dgvr.Cells["DETERMINE"].Value = "Yes";
                        }
                        if (dr["DETERMINE"].ToString() == "1")
                        {
                            //dgvr.Cells["DETERMINE"].Value = "否";
                            dgvr.Cells["DETERMINE"].Value = "No";
                        }
                        //dgvr.Cells["image_guid"].Value = dr["IMAGE_GUID"].ToString();
                        i++;
                    }
                    GenClass.AutoSizeColumn(dataGridView1);


                }
                else
                {
                    throw new Exception("The material appearance inspection standard is not maintained, please check");
                }
                this.dataGridView1.ClearSelection();

                //this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        static bool iscount(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[0-9]\d*$");
            return reg.IsMatch(str);
        }
        //加载检验水平信息
        private void cbl_view()
        {

            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.VMaterialinventory",//类名
                                            "GetAQLEnum",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["AQL_Level"].ToString());
                DataTable dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["AQL_Rank"].ToString());
                cbo_jysp.DataSource = dt;//加载检验水平下拉框
                cbo_jysp.ValueMember = "ENUM_CODE";
                cbo_jysp.DisplayMember = "ENUM_VALUE";
                cbo_jysp.SelectedIndex = -1;

                cbo_AQLjb.DataSource = dt2;//加载aql级别
                cbo_AQLjb.ValueMember = "ENUM_CODE";
                cbo_AQLjb.DisplayMember = "ENUM_VALUE";
                cbo_AQLjb.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {

                if (iscount(txt_pass_qty.Text))
                {
                  
                    if (Convert.ToDecimal(txt_pass_qty.Text) > Convert.ToDecimal(lab_jcsl.Text))
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("The qualified quantity cannot be greater than the incoming quantity！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        return;
                    }
                }
                string checkeds = string.Empty;

                if (lab_PF.Text == "PASS")
                {
                    checkeds = "0";
                }
                if (lab_PF.Text == "FAIL")
                {
                    checkeds = "1";
                }
                if (img_list.Count > 0)
                {
                    foreach (imginfo item in img_list)
                    {
                        if (!string.IsNullOrWhiteSpace(item.image_url))
                        {
                            UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, item.image_url, Program.Client.UserToken);
                            if (res.IsSuccess)
                            {
                                var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                                item.guid = resultDIC["guid"].ToString();
                            }
                            else
                            {

                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("An error occurred during image upload, please check", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                            }
                        }
                    }
                }
                Dictionary<string, object> p = new Dictionary<string, object>();

                Dictionary<string, object> a = new Dictionary<string, object>();
                if (string.IsNullOrWhiteSpace(dics["CHK_NO"].ToString()) ||
                    string.IsNullOrWhiteSpace(txt_cysl.Text) ||
                    string.IsNullOrWhiteSpace(txt_pass_qty.Text) ||
                    string.IsNullOrWhiteSpace(cbo_jysp.Text) ||
                    string.IsNullOrWhiteSpace(cbo_AQLjb.Text) ||
                    string.IsNullOrWhiteSpace(txt_acre.Text) ||
                    string.IsNullOrWhiteSpace(txt_code.Text) ||
                    string.IsNullOrWhiteSpace(lab_name.Text))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("There is still unfilled data, please confirm！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                if (txt_code.Text != usercode)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("There is an error in the inspector's entry process, please check！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                a.Add("chk_seq", dics["CHK_SEQ"]);//材料序号
                a.Add("chk_no", dics["CHK_NO"]);//来料单号
                a.Add("item_no", dics["ITEM_NO"]);//料号
                a.Add("remark", txt_remark.Text);//备注
                a.Add("id", ids);
                a.Add("pass_qty", txt_pass_qty.Text);//合格数量
                a.Add("determine", checkeds);//判断
                a.Add("sample_qty", txt_cysl.Text);//抽样数量
                a.Add("testlevel", cbo_jysp.SelectedValue);//检验水平
                a.Add("aql_level", cbo_AQLjb.SelectedValue);//AQL级别
                a.Add("txt_acre", txt_acre.Text);//ac_re
                a.Add("usercode", txt_code.Text);//检查员id
                a.Add("datas", lab_data.Text);//检验日期
                a.Add("rcpt_qty", dics["IV_QTY"]);//进仓=检验数量
                int i = 0;
                if (dataGridView1.Rows.Count > 0)
                {
                    List<qcm_iqc_insp_res_d> b = new List<qcm_iqc_insp_res_d>();
                    qcm_iqc_insp_res_d c = new qcm_iqc_insp_res_d();
                    foreach (DataGridViewRow dgr in dataGridView1.Rows)
                    {
                        //检测项目
                        if (dgr.Cells["test_item_name"].Value != null && dgr.Cells["test_standard"].Value != null && dgr.Cells["determine"].Value != null)
                        {
                            c.chk_no = dics["CHK_NO"].ToString();//收料单号
                            c.badproblem_code = dgr.Cells["badproblem_code"].Value.ToString();//不良原因代号
                            c.test_standard = dgr.Cells["test_standard"].Value.ToString();//检验标准
                            c.determine = dgr.Cells["determine"].Value.ToString();//检验结果
                            c.test_item_name = dgr.Cells["test_item_name"].Value.ToString();//检验项名称
                            c.test_item_no = dgr.Cells["test_item_no"].Value.ToString();
                            c.image_list = img_list.Where(db => db.test_item_no == dgr.Cells["test_item_no"].Value.ToString()).ToList();
                            foreach (DataRow item in GetShoeShape_EditFile(dgr.Cells["CHK_NO"].Value.ToString(), dics["ITEM_NO"].ToString(), dics["CHK_SEQ"].ToString(), dgr.Cells["test_item_no"].Value.ToString()).Rows)
                            {
                                c.image_list.Add((new imginfo
                                {
                                    test_item_no = dgr.Cells["test_item_no"].Value.ToString(),
                                    image_url = item["net_file_url"].ToString(),
                                    guid = item["guid"].ToString(),
                                    chk_no = dgr.Cells["CHK_NO"].Value.ToString()
                                }));
                            }
                        }
                        else
                        {
                            throw new Exception("Test item name, test standard, test result cannot be empty! Please enter and save");
                        }
                        b.Add(c);
                        c = new qcm_iqc_insp_res_d();
                    }
                    var v = new
                    {
                        listdic2 = b
                    };
                    p.Add("p", a);
                    p.Add("p2", v);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                               "SJeMES_IQC",//类库名
                                                "SJeMES_IQC.VMaterialinventory",//类名
                                                "CheckResultAdd",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        MessageBox.Show(ret.ErrMsg);
                    }
                    else
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Successful operation！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        Getlist();
                        image_listimg = new List<imginfo2>();
                        button1.Enabled = true;//恢复取消的操作
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);

                        image_listimg.Clear();
                        img_list.Clear();

                        if (radioButton_fail.Checked)
                        {

                            if (!string.IsNullOrWhiteSpace(dics["ITEM_TYPE_NO"].ToString()))
                            {
                                string ITEM_TYPE_NO = dics["ITEM_TYPE_NO"].ToString().Substring(0, 3);
                                if (!dics.ContainsKey("cysl"))
                                {
                                    dics.Add("cysl", txt_cysl.Text);//抽样数量
                                }
                                if (ITEM_TYPE_NO.Contains("401"))
                                {
                                    F_IQC_Bad_Report_Leather frm = new F_IQC_Bad_Report_Leather(dics, "0", true, (k) => { if (k) { btn_add.Enabled = false; } });//皮料
                                    //frm.Text = "皮料不良报告";//Bad leather report
                                    frm.Text = "Bad leather report";//Bad leather report
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    F_IQC_Bad_Report_NoLeather frm = new F_IQC_Bad_Report_NoLeather(dics, "1", true,"", (k) => { if (k) { btn_add.Enabled = false; } });
                                    //frm.Text = "非皮料不良报告";//Non-leather bad report
                                    frm.Text = "Non-leather bad report";//Non-leather bad report
                                    frm.ShowDialog();
                                }
                            }
                            else
                            {
                                msg = SJeMES_Framework.Common.UIHelper.UImsg("Material type data is missing, please check！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                            }


                        }
                    }
                }
                else
                {

                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please fill in the content and save！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
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

                    int n = 0;
                    //给指定列的下拉框添加SelectedIndexChanged事件
                    /*    DataGridViewComboBoxColumn ss = new DataGridViewComboBoxColumn();*/
                    if (name == "operation")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;

                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("upload_img"))
                        {

                            //创建文件弹出选择窗口（包括文件名）对象
                            OpenFileDialog ofd = new OpenFileDialog();
                            ofd.Multiselect = true;
                            //判断选择的路径
                            string path = string.Empty;
                            ofd.Title = "Please select a folder";
                            ofd.Filter = "image file(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
                            if (ofd.ShowDialog() == DialogResult.OK)
                            {
                                for (int i = 0; i < ofd.FileNames.Length; i++)
                                {
                                    SafeFileName = System.IO.Path.GetFileName(ofd.FileNames[i].ToString());
                                    filePath = ofd.FileNames[i].ToString();
                                    img_list.Add(new imginfo
                                    {
                                        test_item_no = dataGridView1.CurrentRow.Cells["test_item_no"].Value.ToString(),
                                        image_url = filePath,
                                    });
                                    image_listimg.Add(new imginfo2
                                    {
                                        test_item_no = dataGridView1.CurrentRow.Cells["test_item_no"].Value.ToString(),
                                        net_file_url = filePath,
                                        file_name = SafeFileName,
                                    });
                                }

                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Submitted successfully", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);

                            }
                        }
                        else if (cell.CurrentItem.Equals("takephone_img"))
                        {
                            var phRes = new FrmPhotographResult();
                            FrmPhotograph frmTakePh = new FrmPhotograph(phRes);
                            frmTakePh.ShowDialog();
                            if (phRes.IsSuccess)
                            {
                                img_list.Add(new imginfo
                                {
                                    test_item_no = dataGridView1.CurrentRow.Cells["test_item_no"].Value.ToString(),
                                    image_url = phRes.SaveImgPath,
                                });
                                image_listimg.Add(new imginfo2
                                {
                                    test_item_no = dataGridView1.CurrentRow.Cells["test_item_no"].Value.ToString(),
                                    net_file_url = phRes.SaveImgPath,
                                    file_name = phRes.SaveImgName,
                                });

                                //UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, phRes.SaveImgPath, Program.Client.UserToken);
                                //if (res.IsSuccess)
                                //{
                                //    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                                //    var webC = new System.Net.WebClient();
                                //    string url = Program.Client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                                //    Image image = new Bitmap(webC.OpenRead(url));

                                //    System.IO.File.Delete(phRes.SaveImgPath);
                                //}
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
                        else if (cell.CurrentItem.Equals("select_img"))
                        {
                            string CHK_NO = dataGridView1.CurrentRow.Cells["CHK_NO"].Value.ToString();
                            string ID = dataGridView1.CurrentRow.Cells["id"].Value.ToString();//用于查询相应的图片的
                            string TEST_ITEM_NO = dataGridView1.CurrentRow.Cells["test_item_no"].Value.ToString();//检验项编号
                            DataTable dt = new DataTable();
                            if (string.IsNullOrWhiteSpace(ID) || image_listimg.Where(a => a.test_item_no == TEST_ITEM_NO).ToList().Count > 0)
                            {
                                dt = TableExtension.ToDataTable<imginfo2>(image_listimg.Where(a => a.test_item_no == TEST_ITEM_NO).ToList());
                            }
                            else
                            {
                                dt = GetShoeShape_EditFile(CHK_NO, dics["ITEM_NO"].ToString(), dics["CHK_SEQ"].ToString(), TEST_ITEM_NO);
                            }

                            //dt = TableExtension.ToDataTable<imginfo2>(image_listimg.Where(a => a.test_item_no == TEST_ITEM_NO).ToList());
                            //dt.Merge(GetShoeShape_EditFile(CHK_NO, dics["ITEM_NO"].ToString(), dics["CHK_SEQ"].ToString(), TEST_ITEM_NO));
                            using (FrmFileList aa = new FrmFileList(dt, Program.Client.UploadUrl, Program.Client.UserToken))
                            {
                                aa.ShowDialog();
                            }
                            if (image_listimg.Count > 0)
                            {
                                for (int i = 0; i < image_listimg.Count; i++)
                                {
                                    if (image_listimg[i].test_item_no == TEST_ITEM_NO)
                                    {
                                        image_listimg.RemoveAt(i);
                                        i--;
                                    }
                                }
                                for (int i = 0; i < img_list.Count; i++)
                                {
                                    if (img_list[i].test_item_no == TEST_ITEM_NO)
                                    {
                                        img_list.RemoveAt(i);
                                        i--;
                                    }
                                }
                                foreach (DataRow item in dt.Rows)
                                {
                                    //if (string.IsNullOrWhiteSpace(item["id"].ToString()))
                                    //{
                                    image_listimg.Add(new imginfo2
                                    {
                                        test_item_no = TEST_ITEM_NO,
                                        net_file_url = item["net_file_url"].ToString(),
                                        file_name = item["file_name"].ToString(),
                                    });
                                    img_list.Add(new imginfo
                                    {
                                        test_item_no = TEST_ITEM_NO,
                                        image_url = item["net_file_url"].ToString(),
                                    });
                                    //}
                                }
                            }

                        }
                    }
                    else if (dataGridView1.Columns[e.ColumnIndex].Name == "badproblem_code" || dataGridView1.Columns[e.ColumnIndex].Name == "badproblem_name")
                    {
                        string sql = $@"SELECT BADPROBLEM_CODE 问题代号,BADPROBLEM_NAME 问题描述 FROM QCM_IQC_BADPROBLEMS_M ";
                        FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
                        frmData.ShowDialog();
                        if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
                        {
                            dataGridView1.CurrentRow.Cells["badproblem_code"].Value = frmData.RetData.Rows[0]["问题代号"].ToString();
                            dataGridView1.CurrentRow.Cells["badproblem_name"].Value = frmData.RetData.Rows[0]["问题描述"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //返回图片路径
        private DataTable GetShoeShape_EditFile(string CHK_NO, string ITEM_NO, string CHK_SEQ, string TEST_ITEM_NO)
        {
            DataTable dt = new DataTable();
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("CHK_NO", CHK_NO);//guid
                data.Add("ITEM_NO", ITEM_NO);
                data.Add("CHK_SEQ", CHK_SEQ);
                data.Add("TEST_ITEM_NO", TEST_ITEM_NO);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.VMaterialinventory",//类名
                                            "CheckResultCSViewimg",//方法名
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
                    dt.Columns.Add("tablename", typeof(string));
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!string.IsNullOrEmpty(dr["FILE_URL"].ToString()))
                        {
                            try
                            {

                                dr["net_file_url"] = Program.Client.PicUrl + dr["FILE_URL"].ToString();
                                dr["tablename"] = "qcm_iqc_insp_res_d_f";
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
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return dt;
        }

        //下拉框值变动事件
        private void cbo_AQLjb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_jysp.SelectedValue != null && cbo_AQLjb.SelectedValue != null)
            {
                /*if (cbo_jysp.SelectedValue.ToString() == "2" && cbo_AQLjb.SelectedValue.ToString() == "13")
                {
                    return;
                }*/
                xlk_test();
            }

        }
        private void cbo_jysp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_jysp.SelectedValue != null && cbo_AQLjb.SelectedValue != null)
            {
                /*if (cbo_jysp.SelectedValue.ToString() == "2" && cbo_AQLjb.SelectedValue.ToString() == "13")
                {
                    return;
                }*/
                xlk_test();
            }

        }
        private void xlk_test()
        {
            try
            {
                if (cbo_jysp.SelectedValue != null && cbo_AQLjb.SelectedValue != null)
                {
                    //请求api的数据展示
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("qty", lab_jcsl.Text);
                    p.Add("level_code", cbo_jysp.SelectedValue);
                    p.Add("rank_code", cbo_AQLjb.SelectedValue);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJeMES_IQC",//类库名
                                                "SJeMES_IQC.VMaterialinventory",//类名
                                                "GetAQLAcRe2",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        txt_acre.Text = "";
                        txt_cysl.Text = "";
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg($"{ret.ErrMsg}！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    }
                    else
                    {

                        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        if (dic != null)
                        {
                            txt_acre.Text = dic["AC_RE"].ToString();
                            txt_cysl.Text = dic["sample_qty"].ToString();
                        }
                    }


                }

            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
        }
        bool isBind = false;//是否已经绑定事件
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dataGridView1.CurrentCell.OwningColumn.Name == "determine" && dataGridView1.CurrentCell.RowIndex != -1)
            {
                System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)e.Control;

                cb.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            }
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (item.Cells["determine"].EditedFormattedValue.ToString() == "No")//否
                {
                    radioButton_fail.Checked = true;
                    lab_PF.Text = "FAIL";
                    lab_PF.ForeColor = Color.Red;
                    break;
                }
                else
                {
                    lab_PF.Text = "PASS";
                    lab_PF.ForeColor = Color.Green;
                    radioButton_pass.Checked = true;
                }

            }

        }
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && e.RowIndex != -1)
            {
                SendKeys.Send("{F4}");
            }
        }
        private string usercode;
        //检验员编号
        private void txt_code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (!string.IsNullOrEmpty(txt_code.Text))
                    {
                        //带入物料条码
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("STAFF_NO", txt_code.Text);//检验员编号
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                   "SJeMES_IQC",//类库名
                                                    "SJeMES_IQC.VMaterialinventory",//类名
                                                    "CheckResultPDAYCLViewUser2",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));



                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        if (!ret.IsSuccess)
                        {
                            string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                        }
                        else
                        {
                            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["USER_SYS"].ToString());
                            if (dt.Rows.Count > 0)
                            {
                                lab_name.Text = dt.Rows[0]["STAFF_NAME"].ToString();

                                usercode = dt.Rows[0]["STAFF_NO"].ToString();
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
            }
        }

        /// <summary>
        /// 字体超出用.....
        /// </summary>
        /// <param name="label"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        private static void LTooltip(System.Windows.Forms.Label label, int length, string value)
        {
            label.Text = value;
            if (value.Length > length)
            {
                label.Text = label.Text.Substring(0, length) + "...";
            }
            var tip = new ToolTip();
            tip.IsBalloon = false;
            tip.ShowAlways = true;
            tip.SetToolTip(label, value);
        }
        private void txt_pass_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 8 || e.KeyChar == 46))
                e.Handled = true;
            else if (e.KeyChar == 46 && (txt_pass_qty.Text.ToString().Contains(".") || txt_pass_qty.Text.ToString().StartsWith(".")))
            {
                e.Handled = true;
            }
        }

        private void txt_pass_qty_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(lab_jqty.Text) && !string.IsNullOrWhiteSpace(txt_pass_qty.Text))
            {
                if (Convert.ToDecimal(lab_jqty.Text) >= Convert.ToDecimal(txt_pass_qty.Text))
                {
                    lab_jpqty.Text = (Convert.ToDecimal(lab_jqty.Text) - Convert.ToDecimal(txt_pass_qty.Text)).ToString();
                }
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show(" Are you sure to cancel this inspection level? ", " This operation cannot be undone ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {

                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("chk_no", dics["CHK_NO"].ToString());
                    p.Add("ITEM_NO", dics["ITEM_NO"].ToString());
                    p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//料号序号
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                               "SJeMES_IQC",//类库名
                                                "SJeMES_IQC.IQC_Bad_Report",//类名
                                                "GetBad_Report_delete",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        MessageBox.Show(ret.ErrMsg);
                    }
                    else
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Operation canceled successfully", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                        this.Close();

                    }

                }
                catch (Exception ex)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
            }
        }
        public class listdic
        {
            /// <summary>
            /// 项目集合
            /// </summary>
            public List<qcm_iqc_insp_res_d> listdic2 = new List<qcm_iqc_insp_res_d>();
        }

        public class qcm_iqc_insp_res_d
        {
            public string chk_no { get; set; }
            public string test_item_no { get; set; }
            public string test_item_name { get; set; }
            public string test_standard { get; set; }
            public string determine { get; set; }
            public string image_guid { get; set; }
            public string badproblem_code { get; set; }
            public List<imginfo> image_list { get; set; }

        }
        public class imginfo
        {
            public string guid { get; set; }
            public string image_url { get; set; }
            public string chk_no { get; set; }
            public string test_item_no { get; set; }
        }
        public class imginfo2
        {
            public string test_item_no { get; set; }
            public string net_file_url { get; set; }
            public string file_name { get; set; }
            public string file_url { get; set; }
            public string id { get; set; }
            public string tablename { get; set; }
            public string guid { get; set; }

        }


    }
}
