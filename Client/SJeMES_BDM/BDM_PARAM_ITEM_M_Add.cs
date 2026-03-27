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
    public partial class BDM_PARAM_ITEM_M_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string unit = string.Empty;
        DataGridViewRow currRow;
        public BDM_PARAM_ITEM_M_Add()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public BDM_PARAM_ITEM_M_Add(DataGridViewRow _currRow)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            currRow = _currRow;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string param_item_no = string.Empty;
                string workshop_section_no = string.Empty;
                string workshop_section_name = string.Empty;
                string param_item_name = string.Empty;
                string judgment_criteria = string.Empty;
                string check_standard = string.Empty;
                string remarks = string.Empty;
                if (textBox2.Text == "")
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Parameter item number cannot be empty！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                if (textBox3.Text == "")
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Parameter project name cannot be empty！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                if (comboBox1.SelectedIndex==-1)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please select the type of section！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                if (comboBox2.SelectedIndex == -1)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please select the judgment standard！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                if (textBox5.Text == "")
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Test item standard cannot be empty！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("param_item_no", textBox2.Text);//编号
                data.Add("workshop_section_name", comboBox1.Text);//工段种类
                data.Add("workshop_section_no", comboBox1.SelectedValue);//工段种类
                data.Add("param_item_name", textBox3.Text);//名称
                data.Add("judgment_criteria", comboBox2.SelectedValue);//判断标准
                data.Add("check_standard", textBox5.Text);//检测项目标准
                data.Add("remarks", textBox6.Text);//备注
                if (currRow != null)
                    data.Add("id",currRow.Cells["ID"].Value.ToString());//备注

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_PARAM_ITEM_M",//类名
                                            "GetParam_AddOrUpdate",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                string mx_msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, mx_msg);
                this.Close();

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message.ToString(), Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }


        private void GetWorkshop()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_PARAM_ITEM_M",//类名
                                            "GetWorkshop",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                comboBox1.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                comboBox1.DisplayMember = "WORKSHOP_SECTION_NAME";
                comboBox1.ValueMember = "WORKSHOP_SECTION_NO";
                comboBox1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.ToString(), Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }


        private void GetEunm()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_PARAM_ITEM_M",//类名
                                            "GetEunm",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                comboBox2.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                comboBox2.DisplayMember = "enum_value";
                comboBox2.ValueMember = "enum_code";
                comboBox2.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.ToString(), Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void BDM_PARAM_ITEM_M_Add_Load(object sender, EventArgs e)
        {
            GetWorkshop();
            GetEunm();

            if (currRow != null)
            {
                textBox2.Enabled = false;
                textBox2.Text = currRow.Cells["param_item_no"].Value.ToString();
                textBox3.Text = currRow.Cells["param_item_name"].Value.ToString();
                comboBox1.SelectedValue = currRow.Cells["workshop_section_no"].Value.ToString();
                comboBox2.SelectedValue = currRow.Cells["judgment_criteria_code"].Value.ToString();
                textBox5.Text = currRow.Cells["check_standard"].Value.ToString();
                textBox6.Text = currRow.Cells["remark"].Value.ToString();
            }
        }

    }
}
