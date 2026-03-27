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
    public partial class F_BDM_WORKSHOP_SECTION_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string mid = string.Empty;//主表id
        public F_BDM_WORKSHOP_SECTION_Edit()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public F_BDM_WORKSHOP_SECTION_Edit(string _mid)
        {
            InitializeComponent();
            textBox1.ReadOnly = true;
            mid = _mid;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        /// <summary>
        /// 查询检测项目类型
        /// </summary>
        public void Getenum_inspection_type()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Workshop_SectIon",//类名
                                            "Getenum_inspection_type",//方法名
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
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                checkedListBox1.DataSource = dt;
                checkedListBox1.DisplayMember = "enum_value";
                checkedListBox1.ValueMember = "enum_code";
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 查询材料/工序数据源
        /// </summary>
        public void Getenum_data_source()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Workshop_SectIon",//类名
                                            "Getenum_data_source",//方法名
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
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "enum_value";
                comboBox1.ValueMember = "enum_code";
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 修改赋值
        /// </summary>
        public void GetUpdataValue(string id)
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("mid", id);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Workshop_SectIon",//类名
                                            "UpdateWorkshop_SectIon",//方法名
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
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    comboBox1.SelectedValue = dt.Rows[0]["data_source"].ToString();
                    textBox1.Text = dt.Rows[0]["workshop_section_no"].ToString();
                    textBox2.Text = dt.Rows[0]["workshop_section_name"].ToString();
                    textBox3.Text = dt.Rows[0]["product_category"].ToString();
                    textBox4.Text = dt.Rows[0]["remarks"].ToString();
                    List<string> inspection_type = dt.Rows[0]["inspection_type"].ToString().Split(';').ToList();
                    List<int> checkIndexList = new List<int>();
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        string enum_code = ((DataRowView)checkedListBox1.Items[i]).Row["enum_code"].ToString();
                        if (inspection_type.FirstOrDefault(x => x == enum_code) != null)
                            checkIndexList.Add(i);
                    }

                    foreach (var selectIndex in checkIndexList)
                    {
                        checkedListBox1.SetItemChecked(selectIndex, true);
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
        /// 工段创建编辑
        /// </summary>
        public void EditWorkshop_SectIon()
        {
            try
            {
                if (this.textBox1.Text.Trim() != "")
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("mid", mid);
                    data.Add("workshop_section_no", this.textBox1.Text.Trim());
                    data.Add("data_source", this.comboBox1.SelectedValue.ToString());
                    data.Add("workshop_section_name", this.textBox2.Text.Trim());
                    data.Add("product_category", this.textBox3.Text.Trim());
                    data.Add("remarks", this.textBox4.Text.Trim());
                    List<int> inspection_type = new List<int>();
                    foreach (System.Data.DataRowView item in this.checkedListBox1.CheckedItems)
                    {
                        inspection_type.Add(Convert.ToInt32(item.Row["enum_code"].ToString()));
                    }
                    if (inspection_type.Count<=0)
                    {
                        MessageBox.Show("The detection item data source cannot be empty!");
                        return;
                    }
                    data.Add("inspection_type", inspection_type);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.BDM_Workshop_SectIon", "EditWorkshop_SectIon", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                    if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                        this.Close();
                    }
                    else
                        throw new Exception(j["ErrMsg"].ToString());
                }
                else
                    throw new Exception("ID cannot be empty!");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void F_BDM_WORKSHOP_SECTION_Edit_Load(object sender, EventArgs e)
        {
            Getenum_inspection_type();
            Getenum_data_source();
            GetUpdataValue(mid);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditWorkshop_SectIon();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        this.checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                    }
                }

            }
        }
    }
}
