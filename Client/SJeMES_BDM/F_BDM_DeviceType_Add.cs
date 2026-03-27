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
    public partial class F_BDM_DeviceType_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_DeviceType_Main _f_BDM_DeviceType_Main { get; set; }
        public F_BDM_DeviceType_Add()
        {
            InitializeComponent();
            
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(eq_no.Text))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please fill in the equipment number!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("EQ_NO", this.eq_no.Text);
            data.Add("EQ_NAME", this.eq_name.Text);
            data.Add("CORRECTION_FREQUENCY", this.CORRECTION_FREQUENCY.Text);
            data.Add("control_type", combox_eq_type.SelectedValue.ToString());
            data.Add("remark", this.textBox1.Text);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                 "SJ_BDMAPI", "SJ_BDMAPI.BDM_Equipment", "AddEquipment",
                 Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                this.Close();
            }
        }

        private void F_BDM_DeviceType_Add_Load(object sender, EventArgs e)
        {
            #region 赋试穿枚举值
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
            #endregion
        }

        private void CORRECTION_FREQUENCY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ( (e.KeyChar >= '0' && e.KeyChar <= '9')
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
