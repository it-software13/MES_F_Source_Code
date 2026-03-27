using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_IQC
{
    public partial class F_IQC_ConfirmShoes_Store_jcgh : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string qid = string.Empty;
        string ref_standard = string.Empty;
        public F_IQC_ConfirmShoes_Store_jcgh(string _qid,string _ref_standard)
        {
            InitializeComponent();
            qid = _qid;
            ref_standard = _ref_standard;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditConfirmShoes_Store_jc_gh();
            }
        }

        /// <summary>
        /// 编辑-确认鞋-存放管理_借出/归还
        /// </summary>
        public void EditConfirmShoes_Store_jc_gh()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("qid", qid);
                data.Add("ref_standard", ref_standard);
                data.Add("opra_by", textBox1.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJeMES_IQC", "SJeMES_IQC.IQC_ConfirmShoes", "EditConfirmShoes_Store_jc_gh", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
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
