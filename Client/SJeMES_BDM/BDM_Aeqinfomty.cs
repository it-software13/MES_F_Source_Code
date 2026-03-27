using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_AQL.Common;
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
    public partial class BDM_Aeqinfomty :MaterialForm
    {

        private readonly MaterialSkinManager materialSkinManager;
        private Dictionary<string, object> dic = new Dictionary<string, object>();
        public BDM_Aeqinfomty(Dictionary<string, object> _dic)
        {
            InitializeComponent();
            dic = _dic;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
      Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void BDM_Aeqinfomty_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("code", typeof(string));
            dt.Columns.Add("name", typeof(string));
            DataRow dr= dt.NewRow();
            dr["code"] = "0";
           // dr["name"] = "正常";
            dr["name"] = "Normal";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["code"] = "1";
            //dr["name"] = "报废";
            dr["name"] = "Scrapped";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["code"] = "2";
            //dr["name"] = "送修";
            dr["name"] = "Send_for_repair";
            dt.Rows.Add(dr);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "code";
            if (string.IsNullOrWhiteSpace(dic["id"].ToString()))
            {
                MessageBox.Show("Missing basic information maintenance, please check");
                return;
            }
            commit();//初始化数据
            getlist();//显示数据
        }
        public void getlist()
        {


            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                 "SJ_BDMAPI", "SJ_BDMAPI.BDM_Aeqinfom", "GetDataCom",
                 Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dic));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            else
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["Column1"].Value = dr["item_name"].ToString();
                        dgvr.Cells["Column2"].Value = dr["CALIBRATION_STANDARD"].ToString();
                        dgvr.Cells["Column3"].Value = dr["maintain"].ToString();
                        dgvr.Cells["Column4"].Value = dr["remark"].ToString();
                        dgvr.Cells["Column5"].Value = dr["eq_info_no"].ToString();
                        dgvr.Cells["Column6"].Value = dr["eq_no"].ToString();
                        dgvr.Cells["Column7"].Value = dr["id"].ToString();
                        i++;
                    }
                }
                dataGridView1.ClearSelection();
                DataTable dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data2"].ToString());
                if (dt2.Rows.Count > 0)
                {
                    textBox1.Text = dt2.Rows[0]["report_code"].ToString();
                    comboBox1.SelectedValue = dt2.Rows[0]["device_state"].ToString();
                }
            }
        }
        public void commit()
        {
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                "SJ_BDMAPI", "SJ_BDMAPI.BDM_Aeqinfom", "CommitData",
                Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dic));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                DataTable dt = Auxiliary.GetDatagridviewDatable(dataGridView1);
                p.Add("report_code", textBox1.Text);
                p.Add("device_state", comboBox1.SelectedValue);
                p.Add("data", dt);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                           "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Aeqinfom",//类名
                                            "CommitDataMax",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (!Convert.ToBoolean(dic["IsSuccess"].ToString()))
                {
                    MessageBox.Show(dic["ErrMsg"].ToString());
                }
                else
                {
                    MessageBox.Show("Saved successfully");
                    getlist();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
          
        }
    }
}
