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
    public partial class F_BDM_DeviceType_correction_Item : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string _id = string.Empty;
        public F_BDM_DeviceType_correction_Item()
        {
            InitializeComponent();
        }
        public F_BDM_DeviceType_correction_Item(string id)
        {
            _id = id;
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_item_code.Text))
            {
                MessageBox.Show("ID cannot be empty!");
                return;
            }
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("M_ID", _id);//主表关联
            data.Add("ITEM_CODE", this.txt_item_code.Text);
            data.Add("ITEM_NAME", this.txt_item_name.Text);
            data.Add("EQ_TYPE", "0"); //0：校正项目；1：参数项目；

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                 "SJ_BDMAPI", "SJ_BDMAPI.BDM_Equipment", "AddEquipment_type_d",
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

        private void txt_item_code_Click(object sender, EventArgs e)
        {
            F_BDM_DeviceType_CP_Item f = new F_BDM_DeviceType_CP_Item("0");
            f.ShowDialog();
            if (f.Tag != null)
            {
                string code = f.Tag.ToString();
                string name = f.Name.ToString();
                txt_item_code.Text = code;
                txt_item_name.Text = name;
            }
        }
    }
}
