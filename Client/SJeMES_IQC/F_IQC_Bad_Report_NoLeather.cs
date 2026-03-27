using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_IQC
{
    public partial class F_IQC_Bad_Report_NoLeather : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private Dictionary<string, object> dics;
        private string _passqty;
        private Action<bool> _myDelegate;
        private string stacks;
        public F_IQC_Bad_Report_NoLeather(Dictionary<string,object> dic,string stack,bool status,string passqty,Action<bool> myDelegate)
        {
           
            InitializeComponent();
            new List<imginfo>();
            stacks = stack;
            dics = dic;
            _passqty=passqty;
            _myDelegate = myDelegate;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        public F_IQC_Bad_Report_NoLeather(Dictionary<string, object> dic)
        {
            InitializeComponent();
            new List<imginfo>();
            dics = dic;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }
        public F_IQC_Bad_Report_NoLeather(Dictionary<string, object> dic, string status)
        {
            InitializeComponent();
            new List<imginfo>();
            dics = dic;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }
        private void F_IQC_Bad_Report_NoLeather_Load(object sender, EventArgs e)
        {
            lab_gycs.Text = "";
            lab_cgd.Text = "";
            lab_jcrq.Text = "";
            lab_xx.Text = "";
            lab_cysl.Text = "";
            lab_cyl.Text = "";
            lab_clpm.Text = "";
            lab_lh.Text="";
            lab_sld.Text = "";
            confirm_by1.Text = "";
            lab_1.Text = "";
            lab_2.Text = "";
            lab_3.Text = "";
            label16.Text = "";//premika
            confirm_by2.Text = "";
            confirm_by3.Text = "";
            confirm_by4.Text = "";
            label25.Text = "";//premika
            claim_no.Text = "";
            getdateview();
            GetAutograph();
            if (stacks == "VMaterialresults_Add")
            {
                add("1");
            }
           

        }
        /// <summary>
        /// 检验结果展示及信息展示
        /// </summary>
        private void getdateview()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("CHK_NO", dics["CHK_NO"].ToString());//收料单号
                p.Add("ITEM_NO", dics["ITEM_NO"].ToString());//料号
                p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//料号
                p.Add("ORDER_NO", dics["ORDER_NO"].ToString());//采购单号
                p.Add("status", "1");//采购单号
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_Bad_Report",//类名
                                            "GetBad_Report_view2",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData1.ToString());
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data_top"].ToString());//没有保存前的数据，用于预览用
                DataTable dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());//检验报告源
                string insp_report = dic["insp_report"].ToString();//检验报告/不合格说明
                List<qcm_iqc_insp_res_bad_report> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<qcm_iqc_insp_res_bad_report>>(dic["Data2"].ToString());//页面下半数据
                if (dt.Rows.Count > 0)
                {
                    string Content = string.Empty;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["CLOSING_STATUS"].ToString() =="0")
                        {
                            button5.Enabled = false;
                            //button5.Text = "已结案";
                            button5.Text = "Closed";
                            if (stacks == "VMaterialresults_Add")
                            {
                                _myDelegate(true);
                            }
                            button2.Enabled = false;
                        }
                        lab_lh.Text = dt.Rows[i]["ITEM_NO"].ToString();//料号
                        lab_sld.Text = dt.Rows[i]["CHK_NO"].ToString();//料号
                        lab_gycs.Text = dt.Rows[i]["SUPPLIERS_NAME"].ToString();//生产厂商

                        lab_cgd.Text = dt.Rows[i]["ORDER_NO"].ToString();//采购单
                        lab_jcrq.Text = dt.Rows[i]["RCPT_DATE"].ToString();//进仓日期
                        lab_xx.Text = dt.Rows[i]["SHOE_NO"].ToString();//鞋型
                        LTooltip(lab_xx, 25, lab_xx.Text);
                        lab_cysl.Text = dics["cysl"].ToString();//抽检数量
                        decimal cjl = 0;
                        if (dt.Rows[i]["IV_QTY"].ToString() != "" && dt.Rows[i]["IV_QTY"].ToString() != "0")//检验数量
                        {
                            if (dics["cysl"].ToString() != "0"&& dics["cysl"].ToString()!="")
                            {
                                cjl = (Convert.ToDecimal(dics["cysl"].ToString()) / Convert.ToDecimal(dt.Rows[i]["IV_QTY"].ToString())) * 100;
                            }
                        }
                       
                        lab_cyl.Text = cjl.ToString("F") + "%";//抽检率
                        lab_clpm.Text = dt.Rows[i]["NAME_T"].ToString();//材料品名
                        LTooltip(lab_clpm, 66, lab_clpm.Text);
                       
                        txt_jcqty.Text = dt.Rows[i]["RCPT_QTY"].ToString();//进仓数量=收料
                       
                        if (!string.IsNullOrWhiteSpace(_passqty))
                        {
                            txt_blqty.Text = (Convert.ToDecimal(dt.Rows[i]["RCPT_QTY"].ToString()) - Convert.ToDecimal(_passqty)).ToString();//不良数量=>收料-合格;
                        }
                        else
                        {
                            txt_blqty.Text = (Convert.ToDecimal(dt.Rows[i]["RCPT_QTY"].ToString()) - Convert.ToDecimal(dt.Rows[i]["PASS_QTY"].ToString())).ToString();//不良数量=>收料-合格
                        }
                     
                        if (Convert.ToDecimal(txt_blqty.Text) > 0 && Convert.ToDecimal(dt.Rows[i]["RCPT_QTY"].ToString()) > 0)
                        {
                            txt_bll.Text = ((Convert.ToDecimal(dt.Rows[i]["RCPT_QTY"].ToString()) - Convert.ToDecimal(dt.Rows[i]["PASS_QTY"].ToString())) / Convert.ToDecimal(dt.Rows[i]["RCPT_QTY"].ToString()) * 100).ToString("F")+"%";//不良率
                        }
                        else
                        {
                            txt_bll.Text = "0.0%";
                        }
                    }
                    if (dt1.Rows.Count > 0)//检验报告内容区域
                    {
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            if (dt1.Rows[j]["determine"].ToString()=="1")
                            {
                                if (!string.IsNullOrWhiteSpace(dt1.Rows[j]["determine"].ToString()))
                                {
                                   // Content += ("测试项名称：" + dt1.Rows[j]["test_item_name"].ToString() + "、检测标准：" + dt1.Rows[j]["test_standard"].ToString() + "、检验结果：" + (dt1.Rows[j]["determine"].ToString() == "0" ? "PASS" : "FAIL") + "、不良代号：" + dt1.Rows[j]["BADPROBLEM_CODE"].ToString() + "、不良问题：" + dt1.Rows[j]["BADPROBLEM_NAME"].ToString()).ToString() + "\n";
                                    Content += ("Test_Item_Name：" + dt1.Rows[j]["test_item_name"].ToString() + "、Testing_Standard：" + dt1.Rows[j]["test_standard"].ToString() + "、Test_Result：" + (dt1.Rows[j]["determine"].ToString() == "0" ? "PASS" : "FAIL") + "、Bad_Code：" + dt1.Rows[j]["BADPROBLEM_CODE"].ToString() + "、Bad_Question：" + dt1.Rows[j]["BADPROBLEM_NAME"].ToString()).ToString() + "\n";
                                }
                            }
                           
                        }
                       

                    }
                    if (Content.Contains("FAIL"))//有检验报告的时候取里面的值
                    {
                        richTextBox1.Text = Content;
                    }
                    if (!string.IsNullOrWhiteSpace(insp_report))//有修改取修改
                    {
                        richTextBox1.Text = insp_report;//修改了的显示
                    }
                }
                if (list.Count > 0)//下半内容区域
                {
                    foreach (qcm_iqc_insp_res_bad_report item in list)
                    {
                        if(item.spc_mining != null)
                        {
                            txt_tcqty.Text = item.spc_mining;//特采数量
                        }
                        if(item.supplementary_delivery_qty != null)
                        {
                            txt_bsqty.Text = item.supplementary_delivery_qty;//补送数量
                        }
                        richTextBox2.Text = item.return_reason;//退货原因
                        richTextBox3.Text = item.manufacturer_reply;//厂商回复
                        txt_stqty.Text = item.actual_returned_qty;//实退数量
                        if (item.image_list.Count > 0)
                        {
                            foreach (imginfo img_url in item.image_list)
                            {
                                try
                                {
                                    var webC = new System.Net.WebClient();
                                    string url = Program.Client.PicUrl + img_url.image_url;
                                    Image image = new Bitmap(webC.OpenRead(url));
                                    PictureBox pic = new PictureBox();
                                    pic.Image = image;
                                    pic.Width = 160;
                                    pic.Height = 120;
                                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                                    //添加点击事件（预览图片）
                                    pic.Name = url;
                                    pic.Parent = Parent;
                                    pic.Click += new EventHandler(pic_Click);

                                    this.Invoke(new MethodInvoker(delegate
                                    {
                                        this.flowLayoutPanelimg.Controls.Add(pic);
                                        img_list.Add(new imginfo
                                        {
                                            guid = img_url.guid
                                        });
                                    }));
                                }
                                catch (Exception ex)
                                {
                                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Picture not found, need to re-upload the picture!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                                }

                            }
                        }
                    }
                }
                DataTable dt_claim_no = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["dt_claim_no"].ToString());//cimal_no单号
                if (dt_claim_no.Rows.Count > 0)
                {
                    claim_no.Text = dt_claim_no.Rows[0]["claim_no"].ToString();
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }

        }


        public void GetAutograph()
        {
            btn_qk1.Text = "Confirm signature";
            btn_qk1.Enabled = true;
            btn_qk2.Text = "Confirm signature";
            btn_qk2.Enabled = true;
            btn_qk3.Text = "Confirm signature";
            btn_qk3.Enabled = true;
            btn_qk4.Text = "Confirm signature";
            btn_qk4.Enabled = true;
            //premika--start
            button4.Text = "Confirm signature";
            button4.Enabled = true;
            //premika--end
            confirm_by1.Text = "";
            confirm_by2.Text = "";
            confirm_by3.Text = "";
            confirm_by4.Text = "";
            label25.Text = "";//premika
            lab_1.Text = "";
            lab_2.Text = "";
            lab_3.Text = "";
            label16.Text = "";//premika
            txt_code.Text = "";
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("CHK_NO", dics["CHK_NO"].ToString());//收料单号
            p.Add("ITEM_NO", dics["ITEM_NO"].ToString());//料号
            p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//料号
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.IQC_Bad_Report",//类名
                                        "GetBad_Autograph",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData1.ToString());
            if (dt.Rows.Count>0&&dt!=null)
            {
                DataRow[] rows = dt.Select("ISDELETE='0'");
                foreach (DataRow item in rows)
                {
                    //枚举 0：QIP总巡检核准；1：会签业务/仓库股长；2：QIP材料助理复核；3：检验员判定 ;
                    //枚举 0：QIP section head；1：warehouse；2：QIP supervisor；3：inspector ;4:business------------updated flow
                    switch (item["DEPARTMENT"].ToString())
                    {
                        case "0":
                            btn_qk1.Text = "Signed";
                            btn_qk1.Enabled = false;
                            confirm_by1.Text = item["CONFIRM_BY"].ToString();
                            lab_1.Text = item["STAFF_NAME"].ToString();
                            break;
                        case "1":
                            btn_qk2.Text = "Signed";
                            btn_qk2.Enabled = false;
                            confirm_by2.Text = item["CONFIRM_BY"].ToString();
                            lab_2.Text = item["STAFF_NAME"].ToString();
                            break;
                        case "2":
                            btn_qk3.Text = "Signed";
                            btn_qk3.Enabled = false;
                            confirm_by3.Text = item["CONFIRM_BY"].ToString();
                            lab_3.Text = item["STAFF_NAME"].ToString();
                            break;
                        case "3":
                            btn_qk4.Text = "Signed";
                            btn_qk4.Enabled = false;
                            confirm_by4.Text = item["STAFF_NAME"].ToString();
                            txt_code.Text = item["CONFIRM_BY"].ToString();
                            break;
                        //premika--start
                        case "4":
                            button4.Text = "Signed";
                            button4.Enabled = false;
                            label16.Text = item["STAFF_NAME"].ToString();
                            label25.Text = item["CONFIRM_BY"].ToString();
                            break;
                            //premika--end
                    }
                }
            }
        }


        public void GetAdd(string DEPARTMENT,string usercode)
        {


            string msgs = SJeMES_Framework.Common.UIHelper.UImsg("Whether to sign！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
            var isok = SJeMES_Control_Library.MessageHelper.ShowOK(this, msgs);
            if (isok.ToString().ToLower() == "ok")
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("CHK_NO", dics["CHK_NO"].ToString());//收料单号
                p.Add("DEPARTMENT", DEPARTMENT);//收料单号
                p.Add("ITEM_NO", dics["ITEM_NO"].ToString());//料号
                p.Add("USERCODE", usercode);//签名人
                p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//料号
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_Bad_Report",//类名
                                            "GetBad_AutographAdd",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Signed successfully", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                GetAutograph();
            }
        }


        public void GetDelete(string DEPARTMENT,string text)
        {
            if (text == "Signed")
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Whether to cancel signature confirmation", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                var isok = SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                if (isok.ToString().ToLower() == "ok")
                {
                    //请求api的数据展示
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("CHK_NO", dics["CHK_NO"].ToString());//收料单号
                    p.Add("DEPARTMENT", DEPARTMENT);//部门
                    p.Add("ITEM_NO", dics["ITEM_NO"].ToString());//料号
                    p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//料号
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJeMES_IQC",//类库名
                                                "SJeMES_IQC.IQC_Bad_Report",//类名
                                                "GetBad_AutographDelete",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));

                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
               
                    string messg = SJeMES_Framework.Common.UIHelper.UImsg("Unbind successfully", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowOK(this, messg);

                }
            }
            else 
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Not yet signed! Can't untie！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
    SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
    }
    GetAutograph();
        }
        string url_list = string.Empty;
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                //创建文件弹出选择窗口（包括文件名）对象
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = true;//启动多选
                //判断选择的路径
                string path = string.Empty;
                ofd.Title = "Please select a folder";
                ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < ofd.FileNames.Length; i++)
                    {
                        SafeFileName = System.IO.Path.GetFileName(ofd.FileNames[i].ToString());
                        filePath = ofd.FileNames[i].ToString();
                        Image image = new Bitmap(filePath);
                        PictureBox pic = new PictureBox();
                        pic.Image = image;
                        pic.Width = 160;
                        pic.Height = 120;
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        //添加点击事件（预览图片）
                        pic.Name = filePath;
                        pic.Parent = Parent;
                        pic.Click += new EventHandler(pic_Click);

                        this.Invoke(new MethodInvoker(delegate
                        {
                            this.flowLayoutPanelimg.Controls.Add(pic);
                            img_list.Add(new imginfo
                            {
                                image_url = filePath,
                            });
                        }));
                    }
                    
                }

            }
            catch (Exception)
            {


            }
        }
        /// <summary>
        /// 照片预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pic_Click(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            if (null == pic) return;
            string url = pic.Name; // 取出url
            FrmShowImg add = new FrmShowImg(url, "");
            add.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            add("1");
        }
        private List<imginfo> img_list= new List<imginfo>();
        /// <summary>
        /// 保存不良报告
        /// </summary>
        /// <param name="opan"></param>
        private void add(string opan)
        {
            try
            {
                //判断图片的操作

                if (img_list.Count>0)
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
                        }
                      
                    }
                }
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("chk_no", dics["CHK_NO"].ToString());//收料单号
                p.Add("warehousing_qty", txt_jcqty.Text);//进仓数量
                p.Add("bad_qty", txt_blqty.Text);//不良数量
                string bad_rate = txt_bll.Text.Replace("%", "");
                p.Add("bad_rate", bad_rate);//不良率
                p.Add("spc_mining", txt_tcqty.Text);//特采数量
                p.Add("actual_returned_qty", txt_stqty.Text);//实退数量
                p.Add("supplementary_delivery_qty", txt_bsqty.Text);//补送数量
                p.Add("insp_report", richTextBox1.Text);//检验报告 
                p.Add("return_reason", richTextBox2.Text);//退货原因 
                p.Add("manufacturer_reply", richTextBox3.Text);//厂商回复 
                p.Add("closing_status", opan);//结案处理操作
                p.Add("ITEM_NO",dics["ITEM_NO"].ToString());//料号
                p.Add("guid_list", img_list);//照片guid集
                p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//料号序号
                                                             
                
                //premika-start
                p.Add("GRADE_I","");
                p.Add("GRADE_II","");
                p.Add("GRADE_III", "");
                p.Add("GRADE_IV","");
                p.Add("GRADE_V","");
                p.Add("GRADE_VI","");
                p.Add("UNQUALIFIED","");
                p.Add("AVG_UTIL_RATE","");
                p.Add("ITEM_NAME", "");//lab_clpm
                p.Add("status", "1");

                //premika-end




                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                           "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_Bad_Report",//类名
                                            "GetBad_Report_add2",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Successful operation", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    this.flowLayoutPanelimg.Controls.Clear();
                    img_list = new List<imginfo>();
                    getdateview();
                    GetAutograph();


                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        /// <summary>
        /// 取消不良报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        /// <summary>
        /// 结案处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (btn_qk4.Text == "Signed")
            {
                if (!string.IsNullOrWhiteSpace(txt_tcqty.Text) ||
                  !string.IsNullOrWhiteSpace(txt_stqty.Text) ||
                  !string.IsNullOrWhiteSpace(txt_bsqty.Text))
                {
                    if (MessageBox.Show(" Do you want to close the case? ", " This operation cannot be undone ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        add("0");//结案就为0
                    }

                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("When the operation is closed, the special collection, real refund, and supplementary delivery quantity cannot be empty！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
            }
            else 
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("The inspector has not decided that the signature does not allow the case to be closed！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        /// <summary>
        /// 取消结案操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to cancel this bad report? ", "Cancel the bad report will delete the claim form", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {

                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("CHK_NO", dics["CHK_NO"].ToString());
                    p.Add("ITEM_NO", dics["ITEM_NO"].ToString());
                    p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//料号序号
                    p.Add("CLAIM_NO", claim_no.Text);//单号
                   // p.Add("CLAIM_NO", claim_no.Text);//单号
                    //premika-start
                    p.Add("STATUS","1");
                    //premika-end

                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                               "SJeMES_IQC",//类库名
                                                "SJeMES_IQC.IQC_Bad_Report",//类名
                                                "GetBad_Report_jiean",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        MessageBox.Show(ret.ErrMsg);
                        getdateview();
                    }
                    else
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Operation canceled successfully", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                        //button5.Text = "结案处理";
                        button5.Text = "Close";
                        button5.Enabled = true;
                        button2.Enabled = true;
                    }

                }
                catch (Exception ex)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            this.flowLayoutPanelimg.Controls.Clear();
            img_list = new List<imginfo>();
        }
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
        public static void ChbTooltip(System.Windows.Forms.CheckBox ck, int length, string value)
        {
            ck.Text = value;
            if (value.Length > length)
            {
                ck.Text = ck.Text.Substring(0, length) + "...";
            }
            var tip = new ToolTip();
            tip.IsBalloon = false;
            tip.ShowAlways = true;
            tip.SetToolTip(ck, value);
        }


        private void button11_Click(object sender, EventArgs e)
        {
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("CHK_NO", dics["CHK_NO"].ToString());//收料单号
            p.Add("ITEM_NO", dics["ITEM_NO"].ToString());//料号
            p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//料号序号
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.IQC_Bad_Report",//类名
                                        "GetBad_Autograph",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData1.ToString());
            F_IQC_Bad_Report_Read frm = new F_IQC_Bad_Report_Read(dt);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void btn_qk1_Click(object sender, EventArgs e)
        {
            GetAdd("0","");
        }

        private void btn_qk2_Click(object sender, EventArgs e)
        {
            GetAdd("1","");
        }

        private void btn_qk3_Click(object sender, EventArgs e)
        {

             GetAdd("2","");
        }

        private string usercode=string.Empty;
        private void btn_qk4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(confirm_by4.Text))
            {
                GetAdd("3",usercode);
            }
            else
            {
                MessageBox.Show("Please enter the inspector first, and then click Confirm Signature");
            }
           
        }

        private void button10_Click(object sender, EventArgs e)
        {
            GetDelete("0", btn_qk1.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {

            if (button4.Text == "Signed")
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Can not Cancel The signature.");
            }
            else
            {
                GetDelete("1", btn_qk2.Text);
            }
            //GetDelete("1", btn_qk2.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (btn_qk2.Text == "Signed")
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Can not Cancel The signature.");
            }
            else
            {
                GetDelete("2", btn_qk3.Text);
            }
            //GetDelete("2", btn_qk3.Text);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (btn_qk3.Text == "Signed")
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Can not Cancel The signature.");
            }
            else
            {
                GetDelete("3", btn_qk4.Text);
            }

            //GetDelete("3", btn_qk4.Text);
        }

        public class qcm_iqc_insp_res_bad_report
        {
            /// <summary>
            /// ID
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 收料单号
            /// </summary>
            public string chk_no { get; set; }
            /// <summary>
            /// 结案状态
            /// </summary>
            public string closing_status { get; set; }
            /// <summary>
            /// 进仓数量
            /// </summary>
            public string warehousing_qty { get; set; }
            /// <summary>
            /// 不良数量
            /// </summary>
            public string bad_qty { get; set; }
            /// <summary>
            /// 不良率
            /// </summary>
            public string bad_rate { get; set; }
            /// <summary>
            /// 特采数量
            /// </summary>
            public string spc_mining { get; set; }
            /// <summary>
            /// 实退数量
            /// </summary>
            public string actual_returned_qty { get; set; }
            /// <summary>
            /// 补送数量
            /// </summary>
            public string supplementary_delivery_qty { get; set; }
            /// <summary>
            /// 检验报告/不合格说明
            /// </summary>
            public string insp_report { get; set; }
            /// <summary>
            /// 退货原因
            /// </summary>
            public string return_reason { get; set; }
            /// <summary>
            /// 厂商回复
            /// </summary>
            public string manufacturer_reply { get; set; }
            /// <summary>
            /// 图片集合
            /// </summary>
            public List<imginfo> image_list { get; set; }
        }
        public class imginfo
        {
            public string guid { get; set; }
            public string image_url { get; set; }
        }

        private void txt_tcqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 8 || e.KeyChar == 46))
                e.Handled = true;
            else if (e.KeyChar == 46 && (txt_tcqty.Text.ToString().Contains(".") || txt_tcqty.Text.ToString().StartsWith(".")))
            {
                e.Handled = true;
            }
        }

        private void txt_bsqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 8 || e.KeyChar == 46))
                e.Handled = true;
            else if (e.KeyChar == 46 && (txt_bsqty.Text.ToString().Contains(".") || txt_bsqty.Text.ToString().StartsWith(".")))
            {
                e.Handled = true;
            }
        }

        private void txt_tcqty_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_blqty.Text) && !string.IsNullOrWhiteSpace(txt_tcqty.Text))
            {
                if (Convert.ToDecimal(txt_blqty.Text) >= Convert.ToDecimal(txt_tcqty.Text))
                {
                    txt_stqty.Text = (Convert.ToDecimal(txt_blqty.Text) - Convert.ToDecimal(txt_tcqty.Text)).ToString();
                }
            }
        }
        private void txt_code_DoubleClick(object sender, EventArgs e)
        {
            string sql = $@"select STAFF_NO STAFF_NO,STAFF_NAME STAFF_NAME from HR001M";
            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                confirm_by4.Text = frmData.RetData.Rows[0]["STAFF_NAME"].ToString();
                txt_code.Text = frmData.RetData.Rows[0]["STAFF_NO"].ToString();
                usercode = frmData.RetData.Rows[0]["STAFF_NO"].ToString();
            }
        }
        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
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
                                confirm_by4.Text = dt.Rows[0]["STAFF_NAME"].ToString();

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
        //premika-start
        private void Button4_Click(object sender, EventArgs e)
        {
            GetAdd("4", "");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (btn_qk1.Text == "Signed")
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Can not Cancel The signature.");
            }
            else
            {
                GetDelete("4", button4.Text);
            }
            //GetDelete("4", button4.Text);
        }
        //premika--end
    }
}
