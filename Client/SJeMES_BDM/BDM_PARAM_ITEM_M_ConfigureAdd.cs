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

    public partial class BDM_PARAM_ITEM_M_ConfigureAdd : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string unit = string.Empty;
        DataTable dtSource = new DataTable();
        public BDM_PARAM_ITEM_M_ConfigureAdd()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                dtSource = dt;
                comboBox2.DataSource = dt.Copy();
                comboBox2.DisplayMember = "WORKSHOP_SECTION_NAME";
                comboBox2.ValueMember = "WORKSHOP_SECTION_NO";
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
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
                if (comboBox1.SelectedIndex == -1)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please select a section！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                if (comboBox2.SelectedIndex == -1)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please select process type！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("config_no", textBox1.Text);//编号
                data.Add("workshop_section_name", comboBox2.Text);//工段种类
                data.Add("workshop_section_no", comboBox2.SelectedValue);//工段种类
                data.Add("workmanship_name", comboBox1.Text);//工段种类
                data.Add("workmanship_code", comboBox1.SelectedValue);//工段种类
                data.Add("note", textBox2.Text);//工段种类
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_PARAM_ITEM_M",//类名
                                            "GetWorkshopConfig_Add",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                string mx_msg = SJeMES_Framework.Common.UIHelper.UImsg("Added successfully", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, mx_msg);
                this.Close();

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.ToString(), Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox2.Text))
            {
                if (dtSource.Rows.Count>0)
                {
                    DataTable dwt = dtSource.Clone();
                    DataRow[] rows = dtSource.Select($@"WORKSHOP_SECTION_NO='{comboBox2.SelectedValue.ToString()}'");
                    foreach (DataRow row in rows)
                    {
                        DataRow dt_new = dwt.NewRow();
                        foreach (var item in dwt.Columns)
                        {
                            dt_new[item.ToString()] = row[item.ToString()];
                        }
                        dwt.Rows.Add(dt_new);
                    }
                    comboBox1.DataSource = dwt;
                    comboBox1.DisplayMember = "WORKMANSHIP_NAME";
                    comboBox1.ValueMember = "WORKMANSHIP_CODE";
                    comboBox1.SelectedIndex = 0;
                }
            }
        }
    }
}
