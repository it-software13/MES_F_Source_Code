using MaterialSkin;
using MaterialSkin.Controls;
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
    public partial class F_BDM_PARTS_TESTITEM_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public Dictionary<string, object> _editInfo = null;
        public List<code_value_OBJ> judge_type = new List<code_value_OBJ>();
        public F_BDM_PARTS_TESTITEM_Edit(Dictionary<string, object> editInfo)
        {
            InitializeComponent();
            _editInfo = editInfo;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public bool flag = false;

        private void F_BDM_PARTS_TESTITEM_Edit_Load(object sender, EventArgs e)
        {
            //SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            //SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            judge_type.Add(new code_value_OBJ() { CODE = "1", VALUE = "Fixed value" });
            judge_type.Add(new code_value_OBJ() { CODE = "2", VALUE = "Upper and lower limits" });
            judge_type.Add(new code_value_OBJ() { CODE = "3", VALUE = "difference" });
            cmb_judge_type.DataSource = judge_type;
            cmb_judge_type.DisplayMember = "VALUE";
            cmb_judge_type.ValueMember = "CODE";

            GetJudge();

            if (_editInfo != null)
            {
                txt_code.Text = _editInfo["code"].ToString();
                txt_code.Enabled = false;
                txt_name.Text = _editInfo["name"].ToString();
                cmb_judge.SelectedValue = _editInfo["judge"].ToString();
                cmb_judge_type.SelectedValue = _editInfo["judge_type"].ToString();

                if (_editInfo["judge_type"].ToString() == "2" || _editInfo["judge_type"].ToString() == "3")
                {
                    txt_value2.Visible = true;
                    flowLayoutPanel1.Visible = true;
                    if (_editInfo["judge_type"].ToString() == "2")
                    {
                        pl_sxx.Visible = true;
                        pl_wcz.Visible = false;
                    }
                    else
                    {
                        pl_sxx.Visible = false;
                        pl_wcz.Visible = true;
                    }
                }
                else
                {
                    flowLayoutPanel1.Visible = false;
                    txt_value2.Visible = false;
                }

                var valuelist = _editInfo["judge_value"].ToString().Split('~').ToList();
                txt_value1.Text = valuelist[0].ToString();
                if (valuelist.Count >= 2)
                {
                    txt_value2.Text = valuelist[1].ToString();
                }
                txt_remark.Text = _editInfo["remark"].ToString();
            }
        }
        public void GetJudge()
        {
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("IfSelectNull", "0");
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_BDMAPI",//类库名
                                        "SJ_BDMAPI.BDM_Inspection",//类名
                                        "GetJudge",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            var JudgeList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<code_value_OBJ>>(ret.RetData);

            cmb_judge.DataSource = JudgeList;
            cmb_judge.DisplayMember = "VALUE";
            cmb_judge.ValueMember = "CODE";
        }

        private void cmb_judge_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_judge_type.SelectedValue.ToString() == "2" || cmb_judge_type.SelectedValue.ToString() == "3")
            {
                flowLayoutPanel1.Visible = true;
                txt_value2.Visible = true;
                if (cmb_judge_type.SelectedValue.ToString() == "2")
                {
                    pl_sxx.Visible = true;
                    pl_wcz.Visible = false;
                }
                else
                {
                    pl_sxx.Visible = false;
                    pl_wcz.Visible = true;
                }
            }
            else
            {
                flowLayoutPanel1.Visible = false;
                txt_value2.Visible = false;
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_code.Text.Trim()))
            {
                MessageBox.Show("Please enter the inspection item number");
                txt_code.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txt_name.Text.Trim()))
            {
                MessageBox.Show("Please enter the inspection item name");
                txt_name.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cmb_judge.SelectedValue.ToString().Trim()))
            {
                MessageBox.Show("Please select judgment criteria");
                cmb_judge.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cmb_judge_type.SelectedValue.ToString().Trim()))
            {
                MessageBox.Show("Please select judgment type");
                cmb_judge_type.Focus();
                return;
            }

            decimal value1 = 0;
            decimal.TryParse(txt_value1.Text.Trim(), out value1);
            if (value1 == 0)
            {
                MessageBox.Show("Please enter the correct format of judgment standard value");
                txt_value1.Focus();
                return;
            }
            if (cmb_judge_type.SelectedValue.ToString() == "2" || cmb_judge_type.SelectedValue.ToString() == "3")
            {
                decimal value2 = 0;
                decimal.TryParse(txt_value2.Text.Trim(), out value2);
                if (value2 == 0)
                {
                    MessageBox.Show("Please enter the correct format of judgment standard value");
                    txt_value2.Focus();
                    return;
                }
            }

            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("id", _editInfo == null ? "" : _editInfo["id"].ToString());
                p.Add("code", txt_code.Text.Trim());
                p.Add("name", txt_name.Text.Trim());
                p.Add("judge", cmb_judge.SelectedValue);
                p.Add("judge_type", cmb_judge_type.SelectedValue);
                if (cmb_judge_type.SelectedValue.ToString() == "2" || cmb_judge_type.SelectedValue.ToString() == "3")
                {
                    p.Add("judge_value", txt_value1.Text.Trim() + "~" + txt_value2.Text.Trim());
                }
                else
                {
                    p.Add("judge_value", txt_value1.Text.Trim());
                }
                p.Add("remark", txt_remark.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.BDM_PARTS_TESTITEM", "SavePARTS_TESTITEM", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Save success!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    flag = true;
                    this.Close();
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
    }
}
