using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.Common;
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
    public partial class F_IQC_Bad_Report_Leather : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private Dictionary<string, object> dics;
        private string _status;
        private bool _frm_add=false;
        private Action<bool> _myDelegate;
        private string stacks;//操作来源
        private string data;
        string ITEM_NO;
        public F_IQC_Bad_Report_Leather(Dictionary<string, object> dic,string status,bool frm_add, Action<bool> myDelegate)
        {
            InitializeComponent();
            _status = status;
            _frm_add = frm_add;
            dics = dic;
            _myDelegate = myDelegate;
            new List<imginfo>();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        public F_IQC_Bad_Report_Leather(Dictionary<string, object> dic, string status)
        {
            InitializeComponent();
            _status = status;
            dics = dic;
            new List<imginfo>();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
//        public F_IQC_Bad_Report_Leather(string item_no, string status,string Data)
//        {
//            InitializeComponent();
//            data = Data;
//             _status = status;
//            ITEM_NO = item_no;
//            new List<imginfo>();
//            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
//Program.SkinThemes, materialSkinManager, this);
//            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language); 
//        }
        private void F_IQC_Bad_Report_Leather_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_status))
            {
                if (_status == "0")
                {
                    groupBox1.Text = "Bad leather report";////皮料不良报告
                }
                else if(_status == "1")
                {
                    groupBox1.Text = "Non-leather bad report";//非皮料不良报告
                }
            }
           
            lab_gycs.Text = "";
            lab_cgd.Text = "";
            lab_jcrq.Text = "";
            lab_art.Text = "";
            lab_cyqty.Text = "";
            lab_cjl.Text = "";
            lab_sld.Text = "";
            lab_clpm.Text = "";
            lab_lh.Text = "";
            confirm_by1.Text = "";
            confirm_by2.Text = "";
            confirm_by3.Text = "";
            confirm_by4.Text = "";

            claim_no.Text = "";

            //premika-start
            label36.Text = "";
            label35.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";



            textBox1.KeyPress += NumericOnly_KeyPress;
            textBox2.KeyPress += NumericOnly_KeyPress;
            textBox3.KeyPress += NumericOnly_KeyPress;
            textBox4.KeyPress += NumericOnly_KeyPress;
            textBox5.KeyPress += NumericOnly_KeyPress;
            textBox6.KeyPress += NumericOnly_KeyPress;
            textBox7.KeyPress += NumericOnly_KeyPress;
            textBox8.KeyPress += NumericOnly_KeyPress;



            //premika-end



            //if (string.IsNullOrWhiteSpace(data))
            //{
            getdateview();
                GetAutograph();

            //}
            //else
            //{ 
            //    getprevoiusdateview(ITEM_NO);
            //    GetAutograph();
            //}
           


        }
        private List<imginfo> img_list = new List<imginfo>();
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
                p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//料号序号
                p.Add("status", _status);//类型（0皮料，1非皮料）
                //p.Add("TYPE", dics["TYPE"].ToString());
              string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_Bad_Report",//类名
                                            "GetBad_Report_view3",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData1.ToString());
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());//没有保存前的数据，用于预览用
                DataTable Dt_txt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Dt_txt"].ToString());//检验报告源
                if (dt.Rows.Count > 0)
                {
                    
                    if (dt.Rows[0]["CLOSING_STATUS"].ToString() == "0")
                    {
                        //button5.Enabled = false;
                        button5.Text = "Closed";//已结案
                        if (_frm_add)//true说明是检验水平那边操作的
                        {
                            _myDelegate(true);
                        }
                        button5.Enabled = false;
                        button2.Enabled = false;
                    }
                  
                    claim_no.Text = dt.Rows[0]["CLAIM_NO"].ToString();//cimal_no单号
                    lab_gycs.Text= dt.Rows[0]["SUPPLIERS_NAME"].ToString();//供应厂商
                    lab_art.Text = dt.Rows[0]["NAME_S2"].ToString();//鞋型部件
                    LTooltip(lab_art, 25, lab_art.Text);
                    lab_lh.Text = dt.Rows[0]["ITEM_NO"].ToString();//料号
                    lab_cgd.Text = dt.Rows[0]["ORDER_NO"].ToString();//采购单号
                    lab_cyqty.Text = dt.Rows[0]["SAMPLE_QTY"].ToString();//抽检数量
                    lab_sld.Text = dt.Rows[0]["CHK_NO"].ToString();//收料单号
                    lab_jcrq.Text = dt.Rows[0]["RCPT_DATE"].ToString();//进仓日期
                    lab_cjl.Text = dt.Rows[0]["CJ_RATE"].ToString()+"%";//抽检率
                    lab_clpm.Text = dt.Rows[0]["NAME_T"].ToString();//材料名称

                    txt_jcqty.Text= dt.Rows[0]["RCPT_QTY"].ToString();//进仓数量
                    txt_blqty.Text= dt.Rows[0]["BAD_QTY"].ToString();//不良数量
                    txt_bll.Text= dt.Rows[0]["BAD_RATE"].ToString()+"%";//不良率
                    txt_tcqty.Text= dt.Rows[0]["SPC_MINING"].ToString();//特采数量
                    txt_stqty.Text= dt.Rows[0]["ACTUAL_RETURNED_QTY"].ToString();//实退数量
                    txt_bsqty.Text = dt.Rows[0]["SUPPLEMENTARY_DELIVERY_QTY"].ToString();//补送数量
                    richTextBox2.Text= dt.Rows[0]["RETURN_REASON"].ToString();//退货原因
                    richTextBox3.Text= dt.Rows[0]["MANUFACTURER_REPLY"].ToString();//厂商回复
                  
                    //premika-start
                    textBox1.Text = dt.Rows[0]["G1"].ToString();
                    textBox2.Text = dt.Rows[0]["G2"].ToString();
                    textBox3.Text = dt.Rows[0]["G3"].ToString();
                    textBox4.Text = dt.Rows[0]["G4"].ToString();
                    textBox5.Text = dt.Rows[0]["G5"].ToString();
                    textBox6.Text = dt.Rows[0]["G6"].ToString();
                    textBox7.Text = dt.Rows[0]["UNQUALIFIED"].ToString();
                    textBox8.Text = dt.Rows[0]["AVG_UTIL_RATE"].ToString();


                    //premika-end




                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["INSP_REPORT"].ToString()))//检验报告
                    {
                        richTextBox1.Text = dt.Rows[0]["INSP_REPORT"].ToString();
                    }
                    else
                    {
                        string Content = string.Empty;
                        if (Dt_txt.Rows.Count > 0)//检验报告内容区域
                        {
                            for (int j = 0; j < Dt_txt.Rows.Count; j++)
                            {
                                if (Dt_txt.Rows[j]["determine"].ToString().ToLower() == "1")
                                {
                                    if (!string.IsNullOrWhiteSpace(Dt_txt.Rows[j]["determine"].ToString()))
                                    {
                                        //Content += ("测试项名称：" + Dt_txt.Rows[j]["test_item_name"].ToString() + "、检测标准：" + Dt_txt.Rows[j]["test_standard"].ToString() + "、检验结果：" + (Dt_txt.Rows[j]["determine"].ToString() == "0" ? "PASS" : "FAIL") + "、不良代号：" + Dt_txt.Rows[j]["BADPROBLEM_CODE"].ToString() + "、不良问题：" + Dt_txt.Rows[j]["BADPROBLEM_NAME"].ToString()).ToString() + "\n";
                                        Content += ("Test_Item_Name：" + Dt_txt.Rows[j]["test_item_name"].ToString() + "、Test_Standard：" + Dt_txt.Rows[j]["test_standard"].ToString() + "、Test_Result：" + (Dt_txt.Rows[j]["determine"].ToString() == "0" ? "PASS" : "FAIL") + "、Bad_Code：" + Dt_txt.Rows[j]["BADPROBLEM_CODE"].ToString() + "、BADPROBLEM_NAME：" + Dt_txt.Rows[j]["BADPROBLEM_NAME"].ToString()).ToString() + "\n";
                                    }
                                }
                            }

                        }
                        if (Content.Contains("FAIL"))
                        {
                            richTextBox1.Text = Content;
                        }
                    }
                  
                }
                List<imginfo> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<imginfo>>(dic["img_list"].ToString());//页面下半图片
                if (list.Count > 0)//下半内容区域
                {
                    foreach (imginfo img_url in list)
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
                                    id = img_url.id
                                }) ;
                            }));
                        }
                        catch (Exception)
                        {
                            string msg = SJeMES_Framework.Common.UIHelper.UImsg("Picture not found, need to re-upload the picture!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
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
        /// 取消结案处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            if (button5.Enabled == true)
            {
                //结案按钮禁用才能操作取消
                return;
            }
            if (MessageBox.Show(" Are you sure to cancel this bad report? ", "*", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {

                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("CHK_NO", dics["CHK_NO"].ToString());
                    p.Add("ITEM_NO", dics["ITEM_NO"].ToString());
                    p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//料号序号
                    p.Add("CLAIM_NO", claim_no.Text);//单号
                    //premika-start
                    p.Add("STATUS", "0");
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
                       
                    }
                    else
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Operation canceled successfully", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                        //button5.Text = "结案处理";
                        button5.Text = "Close";

                        //恢复初始
                        getdateview();
                        GetAutograph();
                        this.flowLayoutPanelimg.Controls.Clear();
                        img_list = new List<imginfo>();

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
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            
            add("1");//未结案的保存
          
        }
       
        private void add(string opan)
        {
            try
            {
                //判断图片的操作

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
                        }

                    }
                }
                //premika-start

                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter a Grade I Value");  
                    textBox1.Focus();
                    return;
                }

               if (string.IsNullOrEmpty(textBox2.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter a Grade II Value");
                    textBox2.Focus();
                    return;
                }

                if(string.IsNullOrEmpty(textBox3.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter a Grade III Value");
                    textBox3.Focus();
                    return;
                }

                if(string.IsNullOrEmpty(textBox4.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter a Grade IV Value");
                    textBox4.Focus();
                    return;
                }

                if(string.IsNullOrEmpty(textBox5.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter a Grade V Value");
                    textBox5.Focus();
                    return;
                }

                if(string.IsNullOrEmpty(textBox6.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter a Grade VI Value");
                    textBox6.Focus();
                    return;
                }

                if(string.IsNullOrEmpty(textBox7.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter a Unqualified Value");
                    textBox7.Focus();
                    return;
                }

                if(string.IsNullOrEmpty(textBox8.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter a Average Utilization Rate Value.");
                    textBox8.Focus();
                    return;
                }
             
                //premika-end

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("chk_no", dics["CHK_NO"].ToString());//收料单号
                p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//料号序号
                p.Add("ITEM_NO", dics["ITEM_NO"].ToString());//料号
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
                p.Add("guid_list", img_list);//照片guid集
                p.Add("status", _status);//皮料0/非皮料1
                p.Add("closing_status", opan);//结案处理操作
                //premika-start
                p.Add("GRADE_I", textBox1.Text);
                p.Add("GRADE_II", textBox2.Text);
                p.Add("GRADE_III", textBox3.Text);
                p.Add("GRADE_IV", textBox4.Text);
                p.Add("GRADE_V", textBox5.Text);
                p.Add("GRADE_VI", textBox6.Text);
                p.Add("UNQUALIFIED", textBox7.Text);
                p.Add("AVG_UTIL_RATE", textBox8.Text);
                p.Add("ITEM_NAME", lab_clpm.Text);//lab_clpm
             
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
        //premika-start
        private void NumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Allow control keys (like Backspace, Delete, etc.)
            if (char.IsControl(e.KeyChar))
                return;

         
            if (e.KeyChar == '.')
            {
                if (textBox.Text.Contains('.'))
                {
                    e.Handled = true; 
                }
                return;
            }

            // Allow only digits
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
                SJeMES_Control_Library.MessageHelper.ShowErr(this,"Only allows digits only");

            }
        }

        //premika-end

        private void F_IQC_Bad_Report_Leather_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var item in flowLayoutPanelimg.Controls)
            {
                ((PictureBox)item).Image.Dispose();
            }
        }
        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        private string url_list = string.Empty;
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                //创建文件弹出选择窗口（包括文件名）对象
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = true;//支持多张图片
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
        //照片预览
        void pic_Click(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            if (null == pic) return;
            string url = pic.Name; // 取出url
            FrmShowImg add = new FrmShowImg(url, "");
            add.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
        public void GetAutograph()
        {
            btn_qk1.Text = "confirm signature";//确定签名
            btn_qk1.Enabled = true;
            btn_qk2.Text = "confirm signature";
            btn_qk2.Enabled = true;
            btn_qk3.Text = "confirm signature";
            btn_qk3.Enabled = true;
            btn_qk4.Text = "confirm signature";
            btn_qk4.Enabled = true;
            //premika--start
            button4.Text = "confirm signature";
            button4.Enabled = true;
            //premika--end
            confirm_by1.Text = "";
            confirm_by2.Text = "";
            confirm_by3.Text = "";
            confirm_by4.Text = "";
            label36.Text = "";//premika
            lab_1.Text = "";
            lab_2.Text = "";
            lab_3.Text = "";
            label35.Text = "";//premika
            txt_code.Text = "";
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("CHK_NO", dics["CHK_NO"].ToString());//收料单号
            p.Add("ITEM_NO", dics["ITEM_NO"].ToString());//料号
            p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//料号序号
            //p.Add("TYPE", dics["TYPE"].ToString());
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
            if(dt.Rows.Count>0&& dt != null)
            {
                DataRow[] rows = dt.Select("ISDELETE='0'");
                foreach (DataRow item in rows)
                {
                    //枚举 0：QIP总巡检核准；1：会签业务/仓库股长；2：QIP材料助理复核；3：检验员判定
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
                            label35.Text = item["STAFF_NAME"].ToString();
                            label36.Text = item["CONFIRM_BY"].ToString();
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
                p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//料号序号
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


        public void GetDelete(string DEPARTMENT, string text)
        {
            //if (text == "已签名")
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
                    p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//料号序号
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
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, messg);

                }
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Not yet signed! Can't untie！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
            }
            GetAutograph();
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
        private string usercode = string.Empty;
        private void btn_qk4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(confirm_by4.Text))
            {
                GetAdd("3", usercode);
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
            public string id { get; set; }
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

        private void button6_Click(object sender, EventArgs e)
        {
           // hhi
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //string Chk_No = lab_sld.Text; 
            //string Item_No = lab_lh.Text;
            //F_IQC_Bad_Report_Leather_Grades_Add Gradesform = new F_IQC_Bad_Report_Leather_Grades_Add(Chk_No, Item_No);

            //// Show Form4
            //Gradesform.Show();
        }
        //premika--start
        private void Button4_Click(object sender, EventArgs e)
        {
            GetAdd("4", "");
        }

        private void Button3_Click_1(object sender, EventArgs e)
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
