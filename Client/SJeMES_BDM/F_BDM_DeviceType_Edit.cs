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
    public partial class F_BDM_DeviceType_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string _eq_no { get; set; }
        public F_BDM_DeviceType_Edit(string eq_no)
        {
            InitializeComponent();
            _eq_no = eq_no;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_BDM_DeviceType_Edit_Load(object sender, EventArgs e)
        {
            #region 赋试穿枚举值
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("enum_code", typeof(string));
                dt.Columns.Add("enum_value", typeof(string));

                //for (int i = 0; i < 5; i++)
                //{
                //    DataRow dr = dt.NewRow();
                //    dr["enum_code"] = i;
                //    switch (i)
                //    {
                //        case 0:
                //            dr["enum_value"] = "全部";
                //            break;
                //        case 1:
                //            dr["enum_value"] = "制程机器";
                //            break;
                //        case 2:
                //            dr["enum_value"] = "检验工具";
                //            break;
                //        case 3:
                //            dr["enum_value"] = "测试设备";
                //            break;
                //        case 4:
                //            dr["enum_value"] = "其他";
                //            break;
                //        default:
                //            break;
                //    }
                //    dt.Rows.Add(dr);
                //}
                for (int i = 0; i < 5; i++)
            {
                DataRow dr = dt.NewRow();
                dr["enum_code"] = i;
                switch (i)
                {
                    case 0:
                        dr["enum_value"] = "All";
                        break;
                    case 1:
                        dr["enum_value"] = "Process_Machine";
                        break;
                    case 2:
                        dr["enum_value"] = "Validation_Tools";
                        break;
                    case 3:
                        dr["enum_value"] = "Test_Equipment";
                        break;
                    case 4:
                        dr["enum_value"] = "Other";
                        break;
                    default:
                        break;
                }
                dt.Rows.Add(dr);
            }
                //List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                //Dictionary<string, object> dic = new Dictionary<string, object>();
                //dic.Add("0", "全部");
                //dic.Add("1", "制程机器");
                //dic.Add("2", "检验工具");
                //dic.Add("3", "测试设备");
                //dic.Add("4", "其他");
                //list.Add(dic);

                combox_eq_type.DataSource = dt;
                if (dt != null && dt.Rows.Count > 0)
                {
                    combox_eq_type.DisplayMember = "enum_value";
                    combox_eq_type.ValueMember = "enum_code";
                }
            }
            #endregion

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("EQ_NO", _eq_no);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                 "SJ_BDMAPI", "SJ_BDMAPI.BDM_Equipment", "GetEquipmentInfo",
                 Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
                throw new Exception(ret.ErrMsg);
            else
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        order.Text = dr["EQ_NO"].ToString();//行号
                        name.Text = dr["EQ_NAME"].ToString();//行号
                        txt_correction.Text = dr["CORRECTION_FREQUENCY"].ToString();//编号
                        txt_Remark.Text = dr["REMARK"].ToString();//名称
                        combox_eq_type.SelectedValue = dr["control_type"].ToString();
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("EQ_NO", _eq_no);
            data.Add("EQ_NAME", name.Text);
            data.Add("CORRECTION_FREQUENCY", txt_correction.Text);
            data.Add("control_type", combox_eq_type.SelectedValue.ToString());
            data.Add("REMARK", txt_Remark.Text);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                 "SJ_BDMAPI", "SJ_BDMAPI.BDM_Equipment", "UpdateEquipmentInfo",
                 Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
                throw new Exception(ret.ErrMsg);
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                this.Close();
            }
        }

        private void txt_correction_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9')
                || (e.KeyChar == 8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
