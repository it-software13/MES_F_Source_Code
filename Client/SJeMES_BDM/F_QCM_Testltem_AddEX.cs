using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Controls;
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

namespace SJeMES_BDM
{
    public partial class F_QCM_Testltem_AddEX : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string testitem_type;
        string testitem_codeby;
        string unit = string.Empty;
        public F_QCM_Testltem_AddEX(string testitem_type, string testitem_code,string unit)
        {
            this.testitem_type = testitem_type;
            this.testitem_codeby = testitem_code;
            this.unit = unit;
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_QCM_Testltem_AddEX_Load(object sender, EventArgs e)
        {
            lab_CM.Text = unit;
            try
            {
                switch (testitem_type)
                {
                    case enum_testitem_type.enum_testitem_type_1:
                        txt_min_value.Visible = false;
                        textBox2.Visible = false;
                        txt_max_value.Visible = false;
                        txt_value.Visible = true;
                        break;
                    case enum_testitem_type.enum_testitem_type_2:
                        textBox2.Text = "～";
                        break;
                    case enum_testitem_type.enum_testitem_type_3:
                        textBox2.Text = "±";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            string Tx_text = "";
            //固定值
            string Left_text = "";
            //左边输入框
            string Right_text = "";
            //右边输入框
            string FAGG = "";
            //代号操作
            switch (testitem_type)
            {
                case enum_testitem_type.enum_testitem_type_1:
                    if (!string.IsNullOrEmpty(txt_value.Text.Trim()))
                    {
                        Tx_text = txt_value.Text.Trim();
                        FAGG = "1";
                        Add_serca(Tx_text, Left_text, Right_text, FAGG, testitem_codeby);
                    }
                    else
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("必填项不能为空！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        return;
                    }
                    break;
                case enum_testitem_type.enum_testitem_type_2:
                    if (!string.IsNullOrEmpty(txt_min_value.Text.Trim())&&
                        !string.IsNullOrEmpty(txt_max_value.Text.Trim()))
                    {
                        if (Convert.ToInt32(txt_min_value.Text.Trim()) < Convert.ToInt32(txt_max_value.Text.Trim()))
                        {
                            Left_text = txt_min_value.Text;
                            Right_text = txt_max_value.Text;
                            FAGG = "2";
                            Add_serca(Tx_text, Left_text, Right_text, FAGG, testitem_codeby);
                        }
                        else
                        {
                            MessageBox.Show("上下限左边不能大于右边");
                            return;
                        }
                    }
                    else
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("必填项不能为空！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        return;
                    }
                    break;
                case enum_testitem_type.enum_testitem_type_3:
                    if (!string.IsNullOrEmpty(txt_min_value.Text.Trim())&&
                        !string.IsNullOrEmpty(txt_max_value.Text.Trim()))
                    {
                        Left_text = txt_min_value.Text;
                        Right_text = txt_max_value.Text;
                        FAGG = "3";
                        Add_serca(Tx_text, Left_text, Right_text, FAGG, testitem_codeby);
                    }
                    else
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("必填项不能为空！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        return;
                     
                    }
                    break;
                default:
                    break;
            }
        }
        public  void Add_serca(string Tx_text,string Left_text,string Right_text,string FAGG,string testitem_codeby)
        {

            // 新增测试项数据
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("Tx_text", Tx_text.Trim());
                p.Add("Left_text", Left_text.Trim());
                p.Add("Right_text", Right_text.Trim());
                p.Add("richTextBox_remarks", richTextBox_remarks.Text.Trim());
                p.Add("FAGG", FAGG.Trim());
                //testitem_codeby:检测项编号
                p.Add("testitem_codeby", testitem_codeby.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.BDMBASE",//类名
                                            "GetBDM_TESTITEMAddEx",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowOK(this, msg); 
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
}
